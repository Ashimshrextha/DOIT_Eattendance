function loadMyChartData(chartData) {
    const data = chartData.map(res => ({ LeaveTitle: res.LeaveTitle, RemainingLeave: res.RemainingLeave, AvailableLeave: res.AvailableLeave }));
    const chartLabels = chartData.map(res => res.LeaveTitle)
    const ctx = document.getElementById('myChart');
    new Chart(ctx, {
        height: 30,
        width: 100,
        type: 'bar',
        data: {
            labels: chartLabels,
            datasets: [
                {
                    borderWidth: 2,
                    label: 'बाँकी बिदा',
                    backgroundColor: "rgba(246, 39, 53, 1)",
                    data: data.map(item => item.RemainingLeave),
                   
                },
                {
                    borderWidth: 2,
                    backgroundColor: "rgba(254, 193, 32, 1)",
                    label: 'कुल बिदा',
                    data: data.map(item => item.AvailableLeave),
                  
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            responsive: !0,
            barThickness: 35,
            plugins: {
                legend: {
                    display: !1
                }
            },
            scales: {
                xAxes: [{
                    gridLines: {
                        color: "rgba(0, 0, 0, 0)"
                    }
                }],
                y: {
                    min: 0
                }
            }
        }
    });
}
function loadSuperAdminChartData(chartData) {
    const doughnutLabels = [
        'Red',
        'Blue',
        'Yellow'
    ];
    const doughnutData = [300, 50, 100]
    const ctx = document.getElementById('superadminChart');
    new Chart(ctx, {
        height: 30,
        width: 100,
        type: 'doughnut',
        data: {
            labels: doughnutLabels,
            datasets: [{
                label: 'My First Dataset',
                data: [300, 50, 100],
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                    'rgb(255, 205, 86)'
                ],
                hoverOffset: 4
            }]
        },
        options: {
            maintainAspectRatio: false,
        }
   });
}