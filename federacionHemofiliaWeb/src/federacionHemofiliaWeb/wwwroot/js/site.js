var data = {
    labels: [],
    datasets: [
        {
            label: "My First dataset",
            fillColor: "rgba(220,220,220,0.2)",
            strokeColor: "rgba(220,220,220,1)",
            pointColor: "rgba(220,220,220,1)",
            pointStrokeColor: "#fff",
            pointHighlightFill: "#fff",
            pointHighlightStroke: "rgba(220,220,220,1)",
            data: []
        }
    ]
};

//Chart.defaults.global.responsive = true;
function chartData(id) {

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("GET", "/Doctor/pacienteGraph/" + id, false);
    xmlhttp.send(null);

    if (xmlhttp.responseText == "") {
        alert("usuario con id: " + id + " no tiene info que graficar");
    }
    else {
        var dictionary = JSON.parse(xmlhttp.responseText);

        for (var key in dictionary) {
            var value = dictionary[key];
            var correcionFechas = key.split("T");
            var fecha = correcionFechas[0];
            data.labels.push(fecha);
            data.datasets[0].data.push(value)
        }

        var element = document.getElementById(id).getContext("2d");
        var myChart = new Chart(element).Line(data);

    }

    data.labels = [];
    data.datasets[0].data = [];
}

function navigateTo() {
    document.location = "/Doctor/Cita"
}

//registro funcion escoger fecha

document.getElementById(function () {
    document.getElementById('#datetimepicker1').datetimepicker();
});

//--- Mini Slider ------//
$('#myCarousel').carousel({
  interval: 5000
});

$('#carousel-text').html($('#slide-content-0').html());

// When the carousel slides, auto update the text
$('#myCarousel').on('slid.bs.carousel', function (e) {
  var id = $('.item.active').data('slide-number');
  $('#carousel-text').html($('#slide-content-'+id).html());
});
//---------------------//