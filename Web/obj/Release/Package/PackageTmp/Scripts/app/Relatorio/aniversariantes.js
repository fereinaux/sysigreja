function PrintAniversariantes() {
    $.ajax({
        url: '/Participante/GetAniversariantesByEvento',
        data: { EventoId: $("#relatorio-eventoid").val() },
        datatype: "json",
        type: "GET",
        success: (result) => {
            var doc = CriarPDFA4();
            var titulo = `Lista de Aniversariantes`;
            doc = AddCabecalhoEvento(doc, titulo, $("#relatorio-eventoid option:selected").text());
            doc.line(10, 38, 195, 38);

            doc.setFont('helvetica',"bold")
            doc.text(12, 43, "Nome");
            doc.text(92, 43, "Apelido");
            doc.text(142, 43, "Dia");
            doc.text(172, 43, "Idade");

            doc.line(10, 45, 195, 45); 
            doc.setFont('helvetica',"normal")
            height = 50;

            $(result.data).each((index, participante) => {
                doc.text(12, height, participante.Nome);
                doc.text(92, height, participante.Apelido);
                doc.text(142, height, participante.Dia);
                doc.text(174, height, participante.Idade);
                height += 6;
            });

            AddCount(doc, result.data, height);

            printDoc(doc);
        }
    });
}