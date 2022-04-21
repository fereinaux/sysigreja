String.prototype.replaceAll = String.prototype.replaceAll || function (needle, replacement) {
    return this.split(needle).join(replacement);
};

String.prototype.convertToRGB = function () {
    var aRgbHex = this
    var aRgb = [
        parseInt(aRgbHex[1] + aRgbHex[2], 16),
        parseInt(aRgbHex[3] + aRgbHex[4], 16),
        parseInt(aRgbHex[5] + aRgbHex[6], 16)
    ];
    return aRgb;
}

function EncodeUrl(text) {
    return encodeURIComponent(text).replace(/'/g, "%27").replace(/"/g, "%22").replaceAll("%2Fn", "%0A");
}