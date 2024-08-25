// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//VIEW 1 FUNCTIONS
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
//FUNCION MULTIMEDIA 
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

//FUNCION COLOR SEGUN EL FONDO
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
    return (r * 299 + g * 587 + b * 114) / 1000;
}

//FUNCION PARA FECHAS DEL MODAL EDUCACION
document.addEventListener('DOMContentLoaded', function () {
    const currentYear = new Date().getFullYear();
    const startYear = currentYear - 110;
    const endYear = currentYear ;
    const anoInicioSelect = document.getElementById('ano-inicio');
    const anoFinSelect = document.getElementById('ano-fin');
    const mesInicioSelect = document.getElementById('mes-inicio'); 
    const mesFinSelect = document.getElementById('mes-fin'); 

    function populateYearSelect(selectElement, start, end) {
      for (let year = start; year <= end; year++) {
        const option = document.createElement('option');
        option.value = year;
        option.textContent = year;
        selectElement.appendChild(option);
      }
    }
    function populateMonthSelect(selectElement) {
        const months = [
            { value: '', text: 'MES' }, // Opción por defecto
            { value: 'enero', text: 'Enero' },
            { value: 'febrero', text: 'Febrero' },
            { value: 'marzo', text: 'Marzo' },
            { value: 'abril', text: 'Abril' },
            { value: 'mayo', text: 'Mayo' },
            { value: 'junio', text: 'Junio' },
            { value: 'julio', text: 'Julio' },
            { value: 'agosto', text: 'Agosto' },
            { value: 'septiembre', text: 'Septiembre' },
            { value: 'octubre', text: 'Octubre' },
            { value: 'noviembre', text: 'Noviembre' },
            { value: 'diciembre', text: 'Diciembre' }
        ];

        months.forEach(month => {
            const option = document.createElement('option');
            option.value = month.value;
            option.textContent = month.text;
            selectElement.appendChild(option);
        });
    }
  
    populateYearSelect(anoInicioSelect, startYear, endYear);
    populateYearSelect(anoFinSelect, startYear, endYear);
    populateMonthSelect(mesInicioSelect); 
    populateMonthSelect(mesFinSelect);
  });

  //FUNCION PARA INSERTAR EDUCACION / UPDATE INFORMACIÓN
  /*$('#ModalEducacionForm').submit(function(e) {
    e.preventDefault(); 
    console.log($(this).serialize()); 
    
    var id = $('input[name="id"]').val();
    console.log("ID enviado: ", id); 
    var idInfoEmpleado = $('input[name="id_info_empleado"]').val();
    console.log("ID Info Empleado: ", idInfoEmpleado);

    $.ajax({
        type: 'POST',
        url: '/Home/InsertarEducacion/' + id, 
        data: $(this).serialize(),
        success: function(response) {
            console.log("Respuesta recibida: ", response);
            var nuevaListaEducacion = $(response).find('#listaEducacionContainer').html();
            if (nuevaListaEducacion) {
                $('#listaEducacionContainer').html(nuevaListaEducacion); 
                $('#ModalEducacion').modal('hide'); 
                console.log("Modal cerrado y lista actualizada.");
            } else {
                console.log("No se encontró la lista de educación en la respuesta.");
            }
        },
        error: function(error) {
            console.log("Error: ", error);
        }
    });
});*/


//FUNCIÓN PARA PRECARGAR INFORMACIÓN DEL MODAL EDUCACION
$(document).on('click', '.editar-icono', function () {
    var id = $(this).data('id');

    $.ajax({
        url: '/Home/ObtenerDatosEducacion/' + id,
        type: 'GET',
        success: function (response) {
            if (response.success === false) {
                alert(response.message);
            } else {
                console.log("ID obtenido: ", response.id); 
                $('input[name="id"]').val(response.id); 
                $('input[name="titulo"]').val(response.titulo);
                $('input[name="nombre_institucion"]').val(response.nombre_institucion);
                $('select[name="mes_expedicion"]').val(response.mes_expedicion);
                $('select[name="fecha_expedicion"]').val(response.fecha_expedicion);
                $('select[name="mes_caducidad"]').val(response.mes_caducidad);
                $('select[name="fecha_caducidad"]').val(response.fecha_caducidad);
                $('input[name="disciplina_academica"]').val(response.disciplina_academica);
                $('input[name="actividades_grupo"]').val(response.actividades_grupo);
                $('input[name="Descripcion"]').val(response.descripcion);
                $('#ModalEducacion').modal('show');
            }
        },
        error: function (xhr, status, error) {
            console.error("Error al obtener los datos: ", error);
        }
    });
});

$(document).on('click', '.eliminar-icono', function () {
    var id = $(this).data('id');

    $(document).on('click', '.eliminar-icono', function () {
        var id = $(this).data('id');
    
        $.ajax({
            url: '/Home/EliminarEducacion/' + id,
            type: 'POST',  // Si decides usar DELETE, cambia a 'DELETE'
            success: function (response) {
                if (!response.success) {
                    alert(response.message);
                } else {
                    // Elimina el contenedor de la educación del DOM
                    $('.educacion1_container[data-id="' + id + '"]').remove();
    
                    // Verifica si quedan elementos de educación
                    if ($('.educacion1_container').length === 0) {
                        var noEducacionHtml = `
                            <div class="educacion-container">
                                <div class="icon-container">
                                    <i style="margin-left:20%;" class="fa-solid fa-graduation-cap icon-educacion"></i>
                                    <a data-bs-toggle="modal" data-bs-target="#ModalEducacion">
                                        <img class="img_educacion" src="../img/componente/educacion.png" alt="Educación">
                                    </a>
                                </div>
                            </div>`;
                        
                        // Añade el contenido alternativo al DOM
                        $('#listaEducacionContainer').html(noEducacionHtml);
                    }
                }
            },
            error: function (xhr, status, error) {
                console.error("Error al eliminar la educación: ", error);
            }
        });
    });
});

$(document).on('click', '.mas-icono', function () {
    // Limpia los campos del formulario
    $('#ModalEducacionForm').find('input[type="text"], input[type="hidden"], select').val('');
    $('#ModalEducacionForm').find('textarea').val('');
    
    // Muestra el modal
    $('#ModalEducacion').modal('show');
});


/*FUNCION MOSTRAR CONTRASEÑA LOGIN*/
document.getElementById('toggle-password').addEventListener('click', function () {
    const passwordField = document.getElementById('password');
    const passwordToggle = document.getElementById('toggle-password').querySelector('i');

    if (passwordField.type === 'password') {
        passwordField.type = 'text';
        passwordToggle.classList.remove('fa-eye');
        passwordToggle.classList.add('fa-eye-slash');
    } else {
        passwordField.type = 'password';
        passwordToggle.classList.remove('fa-eye-slash');
        passwordToggle.classList.add('fa-eye');
    }
});






