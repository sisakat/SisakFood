// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {
    var charts = $('[id^="calorieChart-"]').each(function() {
        let date = $(this).attr('id').replace("calorieChart-", "");
        let ctx = $(this);
        $.getJSON("/calorieDistribution?at=" + date, function(data) {
            loadCalorieChart(ctx, data);
        });
    });
});

function loadCalorieChart(ctx, data) {
    var chart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['Protein', 'Carbohydrates', 'Fat'],
            datasets: [
                {
                    data: data,
                    backgroundColor: [
                        'rgba(40 , 153, 181, 0.7)',
                        'rgba(40 , 181, 82 , 0.7)',
                        'rgba(181, 63 , 40 , 0.7)'
                    ]
                }
            ]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top'
                },
                title: {
                    display: true,
                    text: 'Calorie distribution'
                }
            }
        }
    });
}