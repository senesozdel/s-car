$(document).ready(function () {
    if (!vehicleChartData || vehicleChartData.length === 0) return;

    const TOTAL_WEEKLY_HOURS = 7 * 24;

    const chartData = vehicleChartData.map(v => ({
        VehicleName: v.VehicleName,
        ActiveHours: parseFloat(v.ActiveHours),
        IdleHours: parseFloat(v.IdleHours),
        ActivePercent: ((v.ActiveHours / TOTAL_WEEKLY_HOURS) * 100).toFixed(1),
        IdlePercent: ((v.IdleHours / TOTAL_WEEKLY_HOURS) * 100).toFixed(1)
    }));

    const vehicleNames = chartData.map(v => v.VehicleName);
    const activeHours = chartData.map(v => v.ActiveHours);
    const idleHours = chartData.map(v => v.IdleHours);
    const activePercents = chartData.map(v => v.ActivePercent);
    const idlePercents = chartData.map(v => v.IdlePercent);

    // Renk paletleri
    const barColors = [
        'rgba(54, 162, 235, 0.8)',
        'rgba(255, 99, 132, 0.8)',
        'rgba(255, 206, 86, 0.8)',
        'rgba(75, 192, 192, 0.8)',
        'rgba(153, 102, 255, 0.8)',
        'rgba(255, 159, 64, 0.8)',
        'rgba(199, 199, 199, 0.8)',
        'rgba(83, 102, 255, 0.8)'
    ];

    const lineColors = [
        'rgba(54, 162, 235, 1)',
        'rgba(255, 99, 132, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)',
        'rgba(199, 199, 199, 1)',
        'rgba(83, 102, 255, 1)'
    ];

    let vehicleWorkChart, idleChart;

    function createVehicleWorkChart() {
        const ctx = document.getElementById('vehicleWorkChart').getContext('2d');
        vehicleWorkChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: vehicleNames,
                datasets: [
                    {
                        label: 'Aktif Çalışma (saat)',
                        data: activeHours,
                        backgroundColor: barColors.slice(0, vehicleNames.length)
                    },
                    {
                        label: 'Aktif Çalışma (%)',
                        data: activePercents,
                        type: 'line',
                        borderColor: lineColors.slice(0, vehicleNames.length),
                        yAxisID: 'y1',
                        tension: 0.3
                    }
                ]
            },
            options: {
                responsive: true,
                interaction: { mode: 'index', intersect: false },
                stacked: false,
                plugins: {
                    legend: { display: true, position: 'bottom' },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                return context.dataset.type === 'line' ? context.parsed.y + '%' : context.parsed.y + ' saat';
                            }
                        }
                    }
                },
                scales: {
                    y: { beginAtZero: true, title: { display: true, text: 'Saat' } },
                    y1: { beginAtZero: true, position: 'right', title: { display: true, text: 'Yüzde (%)' }, grid: { drawOnChartArea: false } }
                }
            }
        });
    }

    function createIdleChart() {
        const ctx = document.getElementById('idleChart').getContext('2d');
        idleChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: vehicleNames,
                datasets: [
                    {
                        label: 'Boşta Bekleme (saat)',
                        data: idleHours,
                        backgroundColor: barColors.slice(0, vehicleNames.length)
                    },
                    {
                        label: 'Boşta Bekleme (%)',
                        data: idlePercents,
                        type: 'line',
                        borderColor: lineColors.slice(0, vehicleNames.length),
                        yAxisID: 'y1',
                        tension: 0.3
                    }
                ]
            },
            options: {
                responsive: true,
                interaction: { mode: 'index', intersect: false },
                stacked: false,
                plugins: {
                    legend: { display: true, position: 'bottom' },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                return context.dataset.type === 'line' ? context.parsed.y + '%' : context.parsed.y + ' saat';
                            }
                        }
                    }
                },
                scales: {
                    y: { beginAtZero: true, title: { display: true, text: 'Saat' } },
                    y1: { beginAtZero: true, position: 'right', title: { display: true, text: 'Yüzde (%)' }, grid: { drawOnChartArea: false } }
                }
            }
        });
    }

    // Başlangıçta ilk grafik
    createVehicleWorkChart();

    $(".chart-btn").on("click", function () {
        const target = $(this).data("target");

        $(".chart-btn").removeClass("active");
        $(this).addClass("active");

        $("canvas").addClass("d-none");
        $("#" + target).removeClass("d-none");

        // Grafikleri lazy render
        if (target === 'vehicleWorkChart' && !vehicleWorkChart) createVehicleWorkChart();
        if (target === 'idleChart' && !idleChart) createIdleChart();
    });
});
