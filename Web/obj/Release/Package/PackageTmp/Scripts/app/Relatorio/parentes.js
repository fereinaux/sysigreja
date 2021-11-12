function PrintParentes() {
    $.ajax({
        url: '/Participante/GetParentesByEvento',
        data: { EventoId: $("#relatorio-eventoid").val() },
        datatype: "json",
        type: "GET",
        success: (result) => {
            var doc = CriarPDFA4();
            var titulo = `Relação de Parentes`;
            doc = AddCabecalhoEvento(doc, titulo, $("#relatorio-eventoid option:selected").text());
            doc.line(10, 38, 195, 38);

            doc.setFont('helvetica',"bold")
            doc.text(12, 43, "Nome");
            doc.text(112, 43, "Círculo");
            doc.text(140, 43, "Parente");            

            doc.line(10, 45, 195, 45); 
            doc.setFont('helvetica',"normal")
            height = 50;
            console.log(result.data);
            $(result.data).each((index, participante) => {
                doc.text(12, height, participante.Nome);
                doc.text(112, height, participante.Circulo != null ? participante.Circulo : "");
                var splitParente = doc.splitTextToSize(participante.Parente, 50);
                doc.text(140, height, splitParente);                
                height += 6 * splitParente.length;
            });

            AddCount(doc, result.data, height);

            printDoc(doc);
        }
    });
}