function PrintFinanceiro() {
    $.ajax({
        url: '/Lancamento/GetConsolidado',
        data: { EventoId: $("#relatorio-eventoid").val() },
        datatype: "json",
        type: "GET",
        success: (result) => {
            var doc = CriarPDFA4();
            var titulo = `Relatório Financeiro - Resumo`;
            doc = AddCabecalhoEvento(doc, titulo, $("#relatorio-eventoid option:selected").text());

            doc.setFont('helvetica',"bold")
            doc.text(12, 43, "Meio Pagamento");
            doc.text(102, 43, "Valor");
            doc.line(10, 45, 195, 45);

            doc.text(12, 50, "Tipo: RECEITA");
            doc.line(10, 52, 195, 52);
            doc.setFont('helvetica',"normal")
            height = 57;

            totalReceber = 0;

            $(result.Consolidado).each((index, lancamento) => {
                if (lancamento.Tipo == "Receber") {
                    doc.text(12, height, lancamento.MeioPagamento);
                    doc.text(102, height, lancamento.Valor.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));
                    totalReceber += lancamento.Valor;
                    height += 6;
                }
            });

            doc.setFont('helvetica',"bold")
            height -= 5;
            doc.line(10, height, 195, height);
            height += 5;
            doc.text(102, height, totalReceber.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));
            height += 2;


            totalPagar = 0;

            height += 10;
            doc.text(12, height, "Tipo: DESPESA");
            height += 2;
            doc.line(10, height, 195, height);
            height += 5;
            doc.setFont('helvetica',"normal")

            $(result.Consolidado).each((index, lancamento) => {
                if (lancamento.Tipo == "Pagar") {
                    doc.text(12, height, lancamento.MeioPagamento);
                    doc.text(102, height, lancamento.Valor.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));
                    totalPagar += lancamento.Valor;
                    height += 6;
                }
            });

            doc.setFont('helvetica',"bold")
            height -= 5;
            doc.line(10, height, 195, height);
            height += 5;
            doc.text(102, height, totalPagar.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));
            height += 12;

            doc.text(88, height, "Saldo");
            doc.text(102, height, (totalReceber - totalPagar).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));

            doc.addPage();
            titulo = `Relatório Financeiro - Detalhado`;
            doc = AddCabecalhoEvento(doc, titulo, $("#relatorio-eventoid option:selected").text());

            $.ajax({
                url: '/Lancamento/GetDetalhado',
                data: { EventoId: $("#relatorio-eventoid").val() },
                datatype: "json",
                type: "GET",
                success: (result) => {
                    doc.setFont('helvetica',"bold")
                    doc.text(12, 43, "Descrição");
                    doc.text(142, 43, "Data");
                    doc.text(172, 43, "Valor");
                    doc.line(10, 45, 195, 45);

                    doc.text(12, 50, "Tipo: RECEITA");
                    doc.line(10, 52, 195, 52);

                    doc.setFont('helvetica',"normal")
                    height = 57;

                    totalReceber = 0;
                    centrocusto = "";
                    totalCentroCusto = 0;
                    $(result.Detalhado).each((index, lancamento) => {
                        if (lancamento.Tipo == "Receber") {
                            if (centrocusto != lancamento.CentroCusto) {
                                if (totalCentroCusto > 0) {
                                    doc.setFont('helvetica',"bold")
                                    height -= 4;
                                    doc.line(169, height, 187, height);
                                    height += 4;
                                    height = SetHeight(height,doc);
                                    doc.text(172, height, totalCentroCusto.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));

                                    doc.setFont('helvetica',"normal")
                                    height += 6;
                                    height = SetHeight(height,doc);
                                    totalCentroCusto = 0;
                                }
                                centrocusto = lancamento.CentroCusto;
                                doc.setFont('helvetica',"bold")
                                doc.text(12, height, `Centro de Custo: ${lancamento.CentroCusto}`);
                                height += 2;
                                height = SetHeight(height,doc);
                                doc.line(10, height, 195, height);
                                height += 5;
                                height = SetHeight(height,doc);
                                doc.setFont('helvetica',"normal")
                            }


                            var splitDescricao = doc.splitTextToSize(lancamento.Descricao, 120);
                            doc.text(12, height, splitDescricao);
                            doc.text(142, height, lancamento.Data);
                            doc.text(172, height, lancamento.Valor.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));
                            totalCentroCusto += lancamento.Valor;
                            totalReceber += lancamento.Valor;
                            height += 6 * splitDescricao.length;
                            height = SetHeight(height,doc);
                        }
                    });

                    if (totalCentroCusto > 0) {
                        doc.setFont('helvetica',"bold")
                        height -= 4;
                        doc.line(169, height, 187, height);
                        height += 4;
                        height = SetHeight(height,doc);
                        doc.text(172, height, totalCentroCusto.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));

                        doc.setFont('helvetica',"normal")
                        height += 6;
                        height = SetHeight(height,doc);
                        totalCentroCusto = 0;
                    }

                    doc.setFont('helvetica',"bold")
                    doc.text(12, height, `Total Receita: ${totalReceber.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}`);
                    height += 2;
                    height = SetHeight(height,doc);

                    totalPagar = 0;

                    height += 10;
                    height = SetHeight(height,doc);
                    doc.text(12, height, "Tipo: DESPESA");
                    height += 2;
                    height = SetHeight(height,doc);
                    doc.line(10, height, 195, height);
                    height += 5;
                    height = SetHeight(height,doc);
                    doc.setFont('helvetica',"normal")
                    centrocusto = "";
                    totalCentroCusto = 0;
                    $(result.Detalhado).each((index, lancamento) => {
                        if (lancamento.Tipo == "Pagar") {
                            if (centrocusto != lancamento.CentroCusto) {
                                if (totalCentroCusto > 0) {
                                    doc.setFont('helvetica',"bold")
                                    height -= 4;
                                    doc.line(169, height, 187, height);
                                    height += 4;
                                    height = SetHeight(height,doc);
                                    doc.text(172, height, totalCentroCusto.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));

                                    doc.setFont('helvetica',"normal")
                                    height += 6;
                                    height = SetHeight(height,doc);
                                    totalCentroCusto = 0;
                                }
                                centrocusto = lancamento.CentroCusto;
                                doc.setFont('helvetica',"bold")
                                doc.text(12, height, `Centro de Custo: ${lancamento.CentroCusto}`);
                                height += 2;
                                height = SetHeight(height,doc);
                                doc.line(10, height, 195, height);
                                height += 5;
                                height = SetHeight(height,doc);
                                doc.setFont('helvetica',"normal")
                            }


                            var splitDescricao = doc.splitTextToSize(lancamento.Descricao, 120);
                            doc.text(12, height, splitDescricao);
                            doc.text(142, height, lancamento.Data);
                            doc.text(172, height, lancamento.Valor.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));
                            totalCentroCusto += lancamento.Valor;
                            totalPagar += lancamento.Valor;
                            height += 6 * splitDescricao.length;
                            height = SetHeight(height,doc);
                        }
                    });

                    if (totalCentroCusto > 0) {
                        doc.setFont('helvetica',"bold")
                        height -= 4;
                        doc.line(169, height, 187, height);
                        height += 4;
                        height = SetHeight(height,doc);
                        doc.text(172, height, totalCentroCusto.toLocaleString('pt-BR', { minimumFractionDigits: 2 }));

                        doc.setFont('helvetica',"normal")
                        height += 6;
                        height = SetHeight(height,doc);
                        totalCentroCusto = 0;
                    }

                    doc.setFont('helvetica',"bold")
                    doc.text(12, height, `Total Despesas: ${totalPagar.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}`);

                    height += 12;
                    height = SetHeight(height,doc);
                    doc.text(12, height, `Saldo: ${(totalReceber - totalPagar).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}`);

                    window.open(doc.output('bloburl'), '_blank');
                }
            });

        }
    });
}

function SetHeight(height, doc) {
    if (height > 285) {
        doc.addPage();
        return 20;
    }
    return height;
}