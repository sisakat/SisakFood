// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getDateString(date) {
    return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear();
}

$(document).ready(function () {
    calorieSummary();
    var charts = $('[id^="calorieChart-"]').each(function () {
        let date = $(this).attr('id').replace("calorieChart-", "");
        let ctx = $(this);
        $.getJSON("calorieDistribution?at=" + date, function (data) {
            loadCalorieChart(ctx, data);
        });
    });
});

function calorieSummary() {
    if ($('#calorieSummaryFromDate').length && $('#calorieSummaryToDate').length && $('#calorieSummary').length) {
        var fromDate = new Date($('#calorieSummaryFromDate').val());
        var fromDateString = getDateString(fromDate);
        var toDate = new Date($('#calorieSummaryToDate').val());
        var toDateString = getDateString(toDate);
        $.getJSON("calorieSummary?from=" + fromDateString + "&to=" + toDateString, function (data) {
            var labels = [];
            var dataPoints = [];
            for (let i = 0; i < data.length; i++) {
                labels.push(new Date(data[i].day).toLocaleDateString());
                dataPoints.push(data[i].calories);
            }

            loadCalorieSummaryChart($('#calorieSummary'), labels, dataPoints);
        });
    }
}

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

var calorieSummaryChart = null;
function loadCalorieSummaryChart(ctx, labels, data) {
    if (calorieSummaryChart)
        refreshCalorieSummaryChart(labels, data);
    calorieSummaryChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Calories',
                    data: data,
                    borderColor: 'rgba(0, 0, 0, 1.0)',
                    fill: false,
                    tension: 0.4
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                title: {
                    display: true,
                    text: 'Calorie summary'
                }
            },
            interaction: {
                intersect: false
            },
            scales: {
                x: {
                    display: true,
                    title: {
                        display: true,
                        text: 'Date'
                    }
                },
                y: {
                    display: true,
                    title: {
                        display: true,
                        text: 'Calories'
                    },
                    suggestedMin: 0,
                    suggestedMax: 2000
                }
            }
        }
    });
}

function refreshCalorieSummaryChart(labels, data) {
    calorieSummaryChart.data.labels = labels;
    calorieSummaryChart.data.datasets[0].data = data;
    calorieSummaryChart.update();
}