// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function mostrarSiguienteSeccion(numeroSeccion) {
    if (numeroSeccion === 1) {
        $('#seccion1').show();
        $('#seccion1-body').show();
        $('#seccion1-footer').show();

        $('#seccion2').hide();
        $('#seccion2-body').hide();
        $('#seccion2-footer').hide();
    } else if (numeroSeccion === 2) {
        $('#seccion2').show();
        $('#seccion2-body').show();
        $('#seccion2-footer').show();

        $('#seccion1').hide();
        $('#seccion1-body').hide();
        $('#seccion1-footer').hide();
    }
}
function updateSlides() {
    const slides = document.getElementsByClassName('carousel-item');
    const carousel = document.getElementById('CarouseLArchivos');
    if (slides.length > 0) {
        carousel.style.display = 'block'; // Muestra el carrusel
        const bootstrapCarousel = new bootstrap.Carousel(carousel);
        document.querySelector('.carousel-control-prev').style.display = 'block';
        document.querySelector('.carousel-control-next').style.display = 'block';
    } else {
        carousel.style.display = 'none'; // Oculta el carrusel
        document.querySelector('.carousel-control-prev').style.display = 'none';
        document.querySelector('.carousel-control-next').style.display = 'none';
    }
}

function adjustTextColorBasedOnBackground() {
    var card = document.querySelector('.card-container');
    var bgColor = window.getComputedStyle(card.querySelector('.rectangulo-encabezado')).backgroundColor;

    var rgb = bgColor.match(/\d+/g);
    var hex = rgb.map(x => {
        var hexPart = parseInt(x).toString(16);
        return hexPart.length === 1 ? '0' + hexPart : hexPart;
    }).join('');

    var brightness = getBrightness(hex);

    var textColor = brightness > 128 ? 'black' : 'white';
    card.style.color = textColor;
    card.querySelectorAll('.telefono, .mail, .ubicacion, .acerca_de_mi_container, #profesion').forEach(el => {
        el.style.color = textColor;
    });
}

document.addEventListener('DOMContentLoaded', adjustTextColorBasedOnBackground);

function getBrightness(hex) {
    hex = hex.replace('#', '');
    var r = parseInt(hex.substring(0, 2), 16);
    var g = parseInt(hex.substring(2, 4), 16);
    var b = parseInt(hex.substring(4, 6), 16);
    return (r * 299 + g * 587 + b * 114) / 1000;}