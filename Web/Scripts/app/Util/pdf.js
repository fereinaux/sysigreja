


function CriarPDFA4() {
    return new jsPDF('p', 'mm', "a4");
}

function AddCabecalhoEvento(doc, titulo, evento) {
   

    doc.setFont('helvetica',"normal")
    doc.setFontSize(12);
    var img = new Image();
    img.src = `/Images/logo-preto.png`;
    doc.addImage(img, 'PNG', 10, 10, 50, 21);
    doc.text(64, 14, evento);
    doc.text(64, 22, titulo);
    doc.text(64, 30, `Data de Impressão: ${moment().format('DD/MM/YYYY HH:mm')}`);

    return doc;
}

function AddCount(doc, data, height, width) {
    height += -3;
    doc.line(10, height, width ? width : 195, height);
    doc.setFont('helvetica',"bold")
    doc.text(12, height + 5, "Total:");
    doc.text(24, height + 5, data.length.toString());
}

function printDoc(doc) {
    doc.autoPrint();
    window.open(doc.output('bloburl'), '_blank');
}