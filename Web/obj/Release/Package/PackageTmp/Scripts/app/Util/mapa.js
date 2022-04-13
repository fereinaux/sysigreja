function getIcon(cor) {
    return L.icon({
        iconUrl: `/Images/mapa/${cor}.png`,
        iconSize: [32, 32],
        iconAnchor: [16, 32], // CHANGE HERE
        popupAnchor: [-5, -35]
    });
}


function createMarkerLayer(map) {
    let markerLayer = new L.layerGroup()
    markerLayer.addTo(map)
    return markerLayer
}


function initMap(id) {
    let tileLayer = L.tileLayer('https://cartodb-basemaps-{s}.global.ssl.fastly.net/light_all/{z}/{x}/{y}.png', { maxZoom: 18, attribution: false })
    return L.map(id,
        {
            zoomControl: true,
            layers: [tileLayer],
            maxZoom: 18,
            minZoom: 6
        })
}