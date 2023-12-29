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
