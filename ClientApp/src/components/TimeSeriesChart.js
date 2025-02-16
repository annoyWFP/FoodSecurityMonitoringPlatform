import React from 'react';
import { Line } from 'react-chartjs-2';
import 'chart.js/auto';

const TimeSeriesChart = ({ data }) => {
    // Expect data: array of objects with { date: 'YYYY-MM-DD', cfii: number }
    const chartData = {
        labels: data.map(item => item.date),
        datasets: [
            {
                label: 'CFII',
                data: data.map(item => item.cfii),
                fill: false,
                borderColor: 'rgba(75,192,192,1)',
                tension: 0.1,
            },
        ],
    };

    return (
        <div>
            {data.length > 0 ? <Line data={chartData} /> : <p>No data available.</p>}
        </div>
    );
};

export default TimeSeriesChart;
