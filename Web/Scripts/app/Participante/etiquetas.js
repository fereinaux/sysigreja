﻿function CarregarEtiquetas() {
    if ($('.tipo:checked').val() == 1) {
        if ($("#etiquetas-participantes").val() != "Pesquisar") {
            $("#etiquetas-participantes").val("Pesquisar").trigger("chosen:updated");
        }
        if ($("#etiquetas-circulos").val() != "Pesquisar") {
            $.ajax({
                url: '/Participante/GetParticipantesByCirculo',
                data: { CirculoId: $("#etiquetas-circulos").val() },
                datatype: "json",
                type: "GET",
                success: (result) => {
<<<<<<< HEAD
                    switch ($('.cracha:checked').val()) {
                        case '1':
                            MontarCrachaParticipantes(result);
                            break;
                        case '2':
                            MontarEtiquetasParticipantes(result);
                            break
                        case '3':
                            MontarTagsParticipantes(result);
                            break;
                        default:
=======
                    if ($('.cracha:checked').val() == 1) {
                        MontarCrachaParticipantes(result);
                    } else {
                        MontarEtiquetasParticipantes(result);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                    }
                }
            });
        } else {
            $.ajax({
                url: '/Participante/GetParticipantesConfirmados',
                data: { EventoId: $("#etiquetas-eventoid").val() },
                datatype: "json",
                type: "POST",
                success: (result) => {
<<<<<<< HEAD
                    switch ($('.cracha:checked').val()) {
                        case '1':
                            MontarCrachaParticipantes(result);
                            break;
                        case '2':
                            MontarEtiquetasParticipantes(result);
                            break
                        case '3':
                            MontarTagsParticipantes(result);
                            break;
                        default:
=======
                    if ($('.cracha:checked').val() == 1) {
                        MontarCrachaParticipantes(result);
                    } else {
                        MontarEtiquetasParticipantes(result);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                    }
                }
            });
        }
    } else {
        if ($("#etiquetas-equipantes").val() != "Pesquisar") {
            $("#etiquetas-equipantes").val("Pesquisar").trigger("chosen:updated");
        }
        if ($("#etiquetas-equipes").val() != "Pesquisar") {
            $.ajax({
                url: '/Equipe/GetMembrosEquipe',
                data: {
                    EventoId: $("#etiquetas-eventoid").val(),
<<<<<<< HEAD
                    EquipeId: $("#etiquetas-equipes").val(),
                    Foto: $('.cracha:checked').val() == 1
=======
                    EquipeId: $("#etiquetas-equipes").val()
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                },
                datatype: "json",
                type: "POST",
                success: (result) => {
                    if ($('.cracha:checked').val() == 1) {
                        MontarCrachaEquipantes(result);
                    } else {
                        MontarEtiquetasEquipantes(result);
                    }

                }
            });
        } else {
            $.ajax({
                url: '/Equipe/GetEquipantesByEvento',
                data: { EventoId: $("#etiquetas-eventoid").val() },
                datatype: "json",
                type: "GET",
                success: (result) => {
                    if ($('.cracha:checked').val() == 1) {
                        MontarCrachaEquipantes(result);
                    } else {
                        MontarEtiquetasEquipantes(result);
                    }
                }
            });
        }
    }
}

<<<<<<< HEAD

function MontarTagsParticipantes(result) {
    var printDoc = new jsPDF('p', 'cm', 'a3');
    printDoc.setFont('Montserrat', 'normal');
    printDoc.setLineWidth(0.1);
    var img = new Image();
    img.src = `/Images/tag-background.jpg`;
    printDoc.addImage(img, 'JPEG', 0, 0, 29.7, 42);

    //printDoc.setFontSize(18);
    let position = 0
    let line = 3
    let number = 0

    $(result.data).each((index, participante) => {
        if (number > 18) {
            printDoc.addPage();
            position = 0
            line = 3
            number = 0
            var img = new Image();
            img.src = `/Images/tag-background.jpg`;
            printDoc.setLineWidth(0.1);
            printDoc.addImage(img, 'JPEG', 0, 0, 29.7, 42);
        }

        switch (position) {
            case 0:
                width = 5
                break;
            case 1:
                width = 12
                break;
            case 2:
                width = 19
                break;
            case 3:
                width = 26
                number++
                break;
            default:
                width = 5
                position = 0
                line += 2
                break;
        }

        position++

        printDoc.setFontSize(16);
        printDoc.setFont('mont', 'normal');
        printDoc.rect(width - 3, line - 1.2, 6, 2);
        printDoc.text(width, line + 0.5, participante.Apelido, 'center');

        printDoc.setFontSize(9.5);
        var splitNome = printDoc.splitTextToSize(participante.Nome, 5.7);
        printDoc.text(width, line - 0.5, splitNome, 'center');

    });
    //printDoc.autoPrint();
    window.open(printDoc.output('bloburl'), '_blank');
}

function MontarEtiquetasParticipantes(result) {
    var printDoc = new jsPDF('p', 'mm', 'letter');
    printDoc.setFont('helvetica', "normal")
=======
function MontarEtiquetasParticipantes(result) {
    var printDoc = new jsPDF('p', 'mm', 'letter');
    printDoc.setFont('helvetica',"normal")
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    printDoc.setFontSize(18);
    $(result.data).each((index, participante) => {
        if (index % 14 == 0 || index == 0) {
            heightApelido = 32;
            heightNome = 42;
            width = 55;
            if (index > 0) {
                printDoc.addPage();
            }
        }
<<<<<<< HEAD
        printDoc.setFont('helvetica', "bold")
        printDoc.setFontSize(18);
        printDoc.text(width, heightApelido, participante.Apelido, 'center');
        printDoc.setFont('helvetica', "normal")
=======
        printDoc.setFont('helvetica',"bold")
        printDoc.setFontSize(18);
        printDoc.text(width, heightApelido, participante.Apelido, 'center');
        printDoc.setFont('helvetica',"normal")
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        printDoc.setFontSize(15);
        var splitNome = printDoc.splitTextToSize(participante.Nome, 70);
        printDoc.text(width, heightNome, splitNome, 'center');
        width = index % 2 == 0 ? 162 : 55;
        heightNome = index % 2 == 0 ? heightNome : heightNome + 33.9;
        heightApelido = index % 2 == 0 ? heightApelido : heightApelido + 33.9;
    });
    printDoc.autoPrint();
    window.open(printDoc.output('bloburl'), '_blank');
}

function MontarEtiquetasEquipantes(result) {
    var printDoc = new jsPDF('p', 'mm', 'a3');
<<<<<<< HEAD
    printDoc.setFont('helvetica', "normal")
=======
    printDoc.setFont('helvetica',"normal")
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    printDoc.setFontSize(18);
    $(result.data).each((index, equipante) => {
        if (index % 14 == 0 || index == 0) {
            heightNome = 42;
            heightApelido = 57;
            heightEquipe = 68;
            width = 73;
            if (index > 0) {
                printDoc.addPage();
            }
        }
<<<<<<< HEAD
        printDoc.setFont('helvetica', "normal")
=======
        printDoc.setFont('helvetica',"normal")
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        printDoc.setFontSize(18);
        printDoc.text(width, heightEquipe, "  (" + equipante.Equipe + ")", 'center');
        var splitNome = printDoc.splitTextToSize(equipante.Nome, 100);
        printDoc.text(width, heightNome, splitNome, 'center');
<<<<<<< HEAD
        printDoc.setFont('helvetica', "bold")
=======
        printDoc.setFont('helvetica',"bold")
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        printDoc.setFontSize(30);
        printDoc.text(width, splitNome.length > 1 ? heightApelido + 4 : heightApelido, equipante.Apelido, 'center');
        width = index % 2 == 0 ? 212 : 73;
        heightNome = index % 2 == 0 ? heightNome : heightNome + 45;
        heightApelido = index % 2 == 0 ? heightApelido : heightApelido + 45;
        heightEquipe = index % 2 == 0 ? heightEquipe : heightEquipe + 45;
    });
    printDoc.autoPrint();
    window.open(printDoc.output('bloburl'), '_blank');
}


function MontarCrachaParticipantes(result) {
    var printDoc = new jsPDF('l', 'cm', 'a3');

    heightFoto = 12;
    widthFoto = 8;
    result.data = result.data.filter(equipante => equipante.Foto)
    var indice = 0
    var xFoto = 0
    var yFoto = 0
    printDoc.addFont("calibri-normal.ttf", "calibri", "normal");
    printDoc.addFont("calibri-bold.ttf", "calibri", "bold");



    $(result.data).each((index, equipante) => {
        if (equipante.Foto) {
            if (index % 10 == 0 || index == 0) {
                indice = 0


                heightNome = 42;
                heightApelido = 57;
                heightEquipe = 68;
                width = 73;
                if (index > 0) {
                    printDoc.addPage();
                }
            }

            if (indice == 0) {
                xFoto = 1
                yFoto = 2.5
            } else if (indice == 1) {
                xFoto = widthFoto + 1
                yFoto = 2.5
            } else if (indice == 2) {
                xFoto = 1
                yFoto = heightFoto + 2.5
            } else if (indice == 3) {
                xFoto = widthFoto + 1
                yFoto = heightFoto + 2.5
            } else if (indice == 4) {
                xFoto = widthFoto * 2 + 1
                yFoto = 0 + 2.5
            } else if (indice == 5) {
                xFoto = widthFoto * 2 + 1
                yFoto = heightFoto + 2.5
            } else if (indice == 6) {
                xFoto = widthFoto * 3 + 1
                yFoto = 0 + 2.5
            } else if (indice == 7) {
                xFoto = widthFoto * 3 + 1
                yFoto = heightFoto + 2.5
            } else if (indice == 8) {
                xFoto = widthFoto * 4 + 1
                yFoto = 0 + 2.5
            } else if (indice == 9) {
                xFoto = widthFoto * 4 + 1
                yFoto = heightFoto + 2.5
            }

            console.log(xFoto, yFoto, widthFoto, heightFoto)
            printDoc.addImage('data:image/jpeg;base64,' + equipante.Foto, 'JPEG', xFoto, yFoto, widthFoto, heightFoto);
            var img = new Image();
            img.src = `/Images/circulos/${equipante.Circulo}.png`;
<<<<<<< HEAD
            printDoc.addImage(img, 'PNG', xFoto, yFoto, widthFoto, heightFoto);

            printDoc.setFontSize(10);
            printDoc.setFont('Montserrat', 'normal');
            printDoc.setTextColor(0, 0, 0)
            printDoc.text(xFoto + widthFoto / 2, yFoto + 11, equipante.Nome, 'center');
            printDoc.setFontSize(18);
            printDoc.setFont('mont', 'normal');
            printDoc.text(xFoto + widthFoto / 2, yFoto + 10.5, equipante.Apelido.toUpperCase(), 'center');
=======
            printDoc.addImage(img, 'PNG', xFoto, yFoto + 7.8, widthFoto, 3.75);
            var img = new Image();
            img.src = `/Images/logo_sombra.png`;
            printDoc.addImage(img, 'PNG', xFoto + 3.8, yFoto + 10.4, 3.55, 1.25);
           
            printDoc.setFontSize(9.8);
            printDoc.setFont('calibri', 'normal');
            printDoc.setTextColor(255, 255, 255)
            printDoc.text(xFoto + 0.4, yFoto + 9.8, equipante.Nome, 'left');
            printDoc.setFontSize(15.5);
            printDoc.setFont('calibri', 'bold');
            printDoc.text(xFoto + 0.4, yFoto + 9.2, equipante.Apelido, 'left');
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
            indice++
        }
    });
    window.open(printDoc.output('bloburl'), '_blank');
}

function MontarCrachaEquipantes(result) {
    var printDoc = new jsPDF('l', 'cm', 'a3');

    heightFoto = 12;
<<<<<<< HEAD
    widthFoto = 8;
=======
    widthFoto = 8   ;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    result.data = result.data.filter(equipante => equipante.Foto)
    var indice = 0
    var xFoto = 0
    var yFoto = 0
<<<<<<< HEAD
=======
    printDoc.addFont("calibri-normal.ttf", "calibri", "normal");
    printDoc.addFont("calibri-bold.ttf", "calibri", "bold");
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

    equipe = ''
    var imgEquipe = new Image();
    var imgSombra = new Image();
<<<<<<< HEAD
    imgSombra.src = `/Images/logo-preto.png`;
=======
    imgSombra.src = `/Images/logo_sombra.png`;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    $(result.data).each((index, equipante) => {
        if (equipante.Foto) {
            if (index % 10 == 0 || index == 0) {
                indice = 0


                heightNome = 42;
                heightApelido = 57;
                heightEquipe = 68;
                width = 73;
                if (index > 0) {
                    printDoc.addPage();
                }
            }

            if (indice == 0) {
                xFoto = 1
                yFoto = 2.5
            } else if (indice == 1) {
                xFoto = widthFoto + 1
                yFoto = 2.5
            } else if (indice == 2) {
                xFoto = widthFoto * 2 + 1
                yFoto = 0 + 2.5
            } else if (indice == 3) {
                xFoto = widthFoto * 3 + 1
                yFoto = 0 + 2.5
            } else if (indice == 4) {
                xFoto = widthFoto * 4 + 1
                yFoto = 0 + 2.5
            } else if (indice == 5) {
<<<<<<< HEAD
                xFoto = 1
=======
                xFoto =  1
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                yFoto = heightFoto + 2.5
            } else if (indice == 6) {
                xFoto = widthFoto + 1
                yFoto = heightFoto + 2.5
            } else if (indice == 7) {
                xFoto = widthFoto * 2 + 1
                yFoto = heightFoto + 2.5
            } else if (indice == 8) {
                xFoto = widthFoto * 3 + 1
                yFoto = heightFoto + 2.5
            } else if (indice == 9) {
                xFoto = widthFoto * 4 + 1
                yFoto = heightFoto + 2.5
            }


            printDoc.addImage('data:image/jpeg;base64,' + equipante.Foto, 'JPEG', xFoto, yFoto, widthFoto, heightFoto);
            if (imgEquipe != equipe) {

<<<<<<< HEAD

                imgEquipe.src = `/Images/equipes/${equipante.Equipe}.png`;
            }
            printDoc.addImage(imgEquipe, 'PNG', xFoto, yFoto, widthFoto, heightFoto);

            printDoc.setTextColor(255, 255, 255)
            printDoc.setFontSize(9.5);
            printDoc.setFont('Montserrat', 'normal');
            printDoc.text(xFoto + widthFoto / 2, yFoto + 10.9, `(${equipante.Equipe.toUpperCase()})`, 'center');
            printDoc.text(xFoto + widthFoto / 2, yFoto + 10.3, equipante.Nome, 'center');
            printDoc.setFontSize(18);
            printDoc.setFont('mont', 'normal');
            printDoc.text(xFoto + widthFoto / 2, yFoto + 9.8, equipante.Apelido.toUpperCase(), 'center');
=======
        
                imgEquipe.src = `/Images/equipes/${equipante.Equipe}.png`;
            }
            printDoc.addImage(imgEquipe, 'PNG', xFoto, yFoto + 7.8, widthFoto, 3.75);
         
            printDoc.addImage(imgSombra, 'PNG', xFoto + 3.8, yFoto + 10.4, 3.55, 1.25);
            switch (equipante.Equipe) {
                case "Secretaria":
                    printDoc.setTextColor(150, 152, 154)
                    break;
                case "Circulação":
                    printDoc.setTextColor(168, 207, 69)
                    break;
                case "Drama":
                    printDoc.setTextColor(135, 65, 150)
                    break;
                case "Bomboniere":
                    printDoc.setTextColor(82, 101,140)
                    break;
                case "Reitores":
                    printDoc.setTextColor(0, 175, 239)
                    break;
                case "Vaila de Apoio Integral":
                    equipante.Equipe = "Vaila"
                    printDoc.setTextColor(255, 242, 18)
                    break;
                case "Grupo de Oração e Encorajamento":
                    equipante.Equipe = 'GOE'
                    printDoc.setTextColor(179, 52, 124)
                    break;
                case "Refeitório":
                    printDoc.setTextColor(223, 52, 56)
                    break;
                case "Mídia":
                    printDoc.setTextColor(237, 47, 89)
                    break;
                case "Louvor":
                    printDoc.setTextColor(242, 108, 53)
                    break;
                case "Pastores":
                    printDoc.setTextColor(254, 254, 254)
                    break;
                case "Vaila de Pequeno Grupo":
                    equipante.Equipe = "PG"
                    printDoc.setTextColor(62, 64, 149)
                    break;
                  
                default:
                    break;
            }
     
            printDoc.setFontSize(9.5);
            printDoc.setFont('calibri', 'normal');
            printDoc.text(xFoto + 0.4, yFoto + 10.2, equipante.Equipe, 'left');
            printDoc.setTextColor(255, 255, 255)
            printDoc.text(xFoto + 0.4, yFoto + 9.6, equipante.Nome, 'left');
            printDoc.setFontSize(15);
            printDoc.setFont('calibri', 'bold');
            printDoc.text(xFoto + 0.4, yFoto + 9, equipante.Apelido, 'left');
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
            indice++
            equipe = equipante.Equipe
        }
    });
    window.open(printDoc.output('bloburl'), '_blank');
}

function CarregarEtiquetaIndividual(position) {
    if ($('.tipo:checked').val() == 1) {
        if ($("#etiquetas-participantes").val() == "Pesquisar") {
            ErrorMessage("Selecione um participante");
        } else {
            $.ajax({
                url: "/Participante/GetParticipante/",
                data: { Id: $("#etiquetas-participantes").val() },
                datatype: "json",
                type: "GET",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    ImprimirIndividual(data, position);
                }
            });
        }
    } else {
        if ($("#etiquetas-equipantes").val() == "Pesquisar") {
            ErrorMessage("Selecione um equipante");
        } else {
            $.ajax({
                url: "/Equipante/GetEquipante/",
                data: { Id: $("#etiquetas-equipantes").val() },
                datatype: "json",
                type: "GET",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    ImprimirIndividual(data, position);
                }
            });
        }
    }
}

function ImprimirIndividual(data, position) {
    var printDoc = new jsPDF('p', 'mm', 'a3');
<<<<<<< HEAD
    printDoc.setFont('helvetica', "normal")
=======
    printDoc.setFont('helvetica',"normal")
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    printDoc.setFontSize(18);
    width = position % 2 == 0 ? 212 : 73;
    multiplicador = 0;

    switch (position) {
        case 1:
            multiplicador = 0;
            break;
        case 2:
            multiplicador = 0;
            break;
        case 3:
            multiplicador = 1;
            break;
        case 4:
            multiplicador = 1;
            break;
        case 5:
            multiplicador = 2;
            break;
        case 6:
            multiplicador = 2;
            break;
        case 7:
            multiplicador = 3;
            break;
        case 8:
            multiplicador = 3;
            break;
        case 9:
            multiplicador = 3;
            break;
        case 10:
            multiplicador = 4;
            break;
        case 11:
            multiplicador = 5;
            break;
        case 12:
            multiplicador = 5;
            break;
        case 13:
            multiplicador = 6;
            break;
        case 18:
            multiplicador = 6;
            break;
        default:
    }
    if ($('.tipo:checked').val() == 1) {
        var participante = data.Participante;
        heightNome = multiplicador * 45;
        heightApelido = multiplicador * 45;
        heightNome += 60;
        heightApelido += 50;
<<<<<<< HEAD
        printDoc.setFont('helvetica', "bold")
        printDoc.setFontSize(30);
        printDoc.text(width, heightApelido, participante.Apelido, 'center');
        printDoc.setFont('helvetica', "normal")
=======
        printDoc.setFont('helvetica',"bold")
        printDoc.setFontSize(30);
        printDoc.text(width, heightApelido, participante.Apelido, 'center');
        printDoc.setFont('helvetica',"normal")
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        printDoc.setFontSize(18);
        var splitNome = printDoc.splitTextToSize(participante.Nome, 100);
        printDoc.text(width, heightNome, splitNome, 'center');

    } else {
        var equipante = data.Equipante;
        heightNome = multiplicador * 45;
        heightApelido = multiplicador * 45;
        heightEquipe = multiplicador * 45;
        heightNome += 42;
        heightApelido += 57;
        heightEquipe += 68;
<<<<<<< HEAD
        printDoc.setFont('helvetica', "normal")
=======
        printDoc.setFont('helvetica',"normal")
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        printDoc.setFontSize(18);
        printDoc.text(width, heightEquipe, "  (" + equipante.Equipe + ")", 'center');
        var splitNome = printDoc.splitTextToSize(equipante.Nome, 100);
        printDoc.text(width, heightNome, splitNome, 'center');
<<<<<<< HEAD
        printDoc.setFont('helvetica', "bold")
=======
        printDoc.setFont('helvetica',"bold")
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        printDoc.setFontSize(30);
        printDoc.text(width, splitNome.length > 1 ? heightApelido + 4 : heightApelido, equipante.Apelido, 'center');
    }
    printDoc.autoPrint();
    window.open(printDoc.output('bloburl'), '_blank');
}

function GetParticipantes() {
    $("#etiquetas-participantes").empty();
    $('#etiquetas-participantes').append($('<option>Pesquisar</option>'));
    $.ajax({
        url: '/Participante/GetParticipantes',
        data: { EventoId: $("#etiquetas-eventoid").val() },
        datatype: "json",
        type: "POST",
        success: (result) => {
            result.data.forEach(function (participante, index, array) {
                if (participante.Status != Cancelado)
                    $('#etiquetas-participantes').append($(`<option value="${participante.Id}">${participante.Nome}</option>`));
            });
            $("#etiquetas-participantes").val("Pesquisar").trigger("chosen:updated");
        }
    });
}

function GetEquipantes() {
    $("#etiquetas-equipantes").empty();
    $('#etiquetas-equipantes').append($('<option>Pesquisar</option>'));
    $.ajax({
        url: '/Equipe/GetEquipantesByEvento',
        data: { EventoId: $("#etiquetas-eventoid").val() },
        datatype: "json",
        type: "GET",
        success: (result) => {
            result.data.forEach(function (equipante, index, array) {
                $('#etiquetas-equipantes').append($(`<option value="${equipante.Id}">${equipante.Nome}</option>`));
            });
            $("#etiquetas-equipantes").val("Pesquisar").trigger("chosen:updated");
        }
    });
}

function GetCirculos() {
    $("#etiquetas-circulos").empty();
    $('#etiquetas-circulos').append($('<option>Pesquisar</option>'));
    $.ajax({
        url: '/Circulo/GetCirculos',
        data: { EventoId: $("#etiquetas-eventoid").val() },
        datatype: "json",
        type: "POST",
        success: (result) => {
            result.data.forEach(function (circulo, index, array) {
                $('#etiquetas-circulos').append($(`<option value="${circulo.Id}">${circulo.Cor}</option>`));
            });
            $("#etiquetas-circulos").val("Pesquisar").trigger("chosen:updated");
        }
    });
}

function GetEquipes() {
    $("#etiquetas-equipes").empty();
    $('#etiquetas-equipes').append($('<option>Pesquisar</option>'));
    $.ajax({
        url: '/Equipe/GetEquipes',
        data: { EventoId: $("#etiquetas-eventoid").val() },
        datatype: "json",
        type: "POST",
        success: (result) => {
            result.data.forEach(function (equipe, index, array) {
                $('#etiquetas-equipes').append($(`<option value="${equipe.Id}">${equipe.Equipe}</option>`));
            });
            $("#etiquetas-equipes").val("Pesquisar").trigger("chosen:updated");
        }
    });
}

$(document).ready(function () {
    HideMenu();
    GetAdjacentes();
});

function GetAdjacentes() {
    GetParticipantes();
    GetCirculos();
    GetEquipantes();
    GetEquipes();
}

$('.tipo').on('ifChanged', function (event) {
    var checkboxChecked = $(this).is(':checked');

    if (checkboxChecked && ($(this).val() == 1)) {
        $('#equipante').addClass('d-none');
        $('#participante').removeClass('d-none');
    } else {
        $('#participante').addClass('d-none');
        $('#equipante').removeClass('d-none');
    }
});

function LimparCampos() {
    $("#etiquetas-participantes").val("Pesquisar").trigger("chosen:updated");
    $("#etiquetas-equipantes").val("Pesquisar").trigger("chosen:updated");
}


