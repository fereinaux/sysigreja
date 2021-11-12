function PrintRestricoes() {
    $.ajax({
        url: '/Participante/GetRestricoesByEvento',
        data: { EventoId: $("#relatorio-eventoid").val() },
        datatype: "json",
        type: "GET",
        success: (result) => {
            var doc = CriarPDFA4();
            var titulo = `Lista de Restrições Alimentares`;
            doc = AddCabecalhoEvento(doc, titulo, $("#relatorio-eventoid option:selected").text());
            doc.line(10, 38, 195, 38);

            doc.setFont('helvetica',"bold")
            doc.text(12, 43, "Nome");
            doc.text(92, 43, "Apelido");
            doc.text(125, 43, "Restrição");

            doc.line(10, 45, 195, 45); 
            doc.setFont('helvetica',"normal")
            height = 50;

            $(result.data).each((index, participante) => {
                doc.text(12, height, participante.Nome);
                doc.text(92, height, participante.Apelido);
                var splitRestricao = doc.splitTextToSize(participante.Restricao, 80);
                doc.text(125, height, splitRestricao);
                height += 6 * splitRestricao.length;
            });

            AddCount(doc, result.data, height);

            printDoc(doc);
        }
    });
}