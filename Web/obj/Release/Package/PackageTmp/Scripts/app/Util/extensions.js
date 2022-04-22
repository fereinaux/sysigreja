String.prototype.replaceAll = String.prototype.replaceAll || function (needle, replacement) {
    return this.split(needle).join(replacement);
};

<<<<<<< HEAD
String.prototype.convertToRGB = function () {
    var aRgbHex = this
    var aRgb = [
        parseInt(aRgbHex[1] + aRgbHex[2], 16),
        parseInt(aRgbHex[3] + aRgbHex[4], 16),
        parseInt(aRgbHex[5] + aRgbHex[6], 16)
    ];
    return aRgb;
}

=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
function EncodeUrl(text) {
    return encodeURIComponent(text).replace(/'/g, "%27").replace(/"/g, "%22").replaceAll("%2Fn", "%0A");
}