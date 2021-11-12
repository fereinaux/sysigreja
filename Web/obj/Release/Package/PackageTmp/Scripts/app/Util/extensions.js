String.prototype.replaceAll = String.prototype.replaceAll || function (needle, replacement) {
    return this.split(needle).join(replacement);
};

function EncodeUrl(text) {
    return encodeURIComponent(text).replace(/'/g, "%27").replace(/"/g, "%22").replaceAll("%2Fn", "%0A");
}