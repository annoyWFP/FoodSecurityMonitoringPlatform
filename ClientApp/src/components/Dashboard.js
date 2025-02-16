import React, { useState, useEffect } from 'react';
import axios from 'axios';
import TimeSeriesChart from './TimeSeriesChart';
import AlertsTable from './AlertsTable';

const Dashboard = () => {
    const [country, setCountry] = useState('YEM'); // Default to Yemen
    const [startDate, setStartDate] = useState('2024-01-01');
    const [endDate, setEndDate] = useState('2024-01-31');
    const [timeSeriesData, setTimeSeriesData] = useState([]);
    const [alerts, setAlerts] = useState([]);

    const fetchData = async () => {
        try {
            // Fetch time series data (expected format: [{ date: 'YYYY-MM-DD', cfii: number }, ...])
            const tsResponse = await axios.get(`/api/FoodSecurity/timeseries/${country}`, {
                params: { startDate, endDate }
            });
            setTimeSeriesData(tsResponse.data);

            // Fetch alerts data (expected format: [{ date, regionName, cfii }, ...])
            const alertsResponse = await axios.get(`/api/FoodSecurity/alerts/${country}`, {
                params: { startDate, endDate }
            });
            setAlerts(alertsResponse.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    // Fetch data on mount and whenever the parameters change.
    useEffect(() => {
        fetchData();
    }, [country, startDate, endDate]);

    return (
        <div>
            <div style={{ marginBottom: '1em' }}>
                <label>
                    Select Country:&nbsp;
                    <select value={country} onChange={(e) => setCountry(e.target.value)}>
                        <option value="YEM">Yemen</option>
                        <option value="SYR">Syria</option>
                    </select>
                </label>
            </div>
            <div style={{ marginBottom: '1em' }}>
                <label>
                    Start Date:&nbsp;
                    <input type="date" value={startDate} onChange={(e) => setStartDate(e.target.value)} />
                </label>
                &nbsp;&nbsp;
                <label>
                    End Date:&nbsp;
                    <input type="date" value={endDate} onChange={(e) => setEndDate(e.target.value)} />
                </label>
                &nbsp;&nbsp;
                <button onClick={fetchData}>Refresh Data</button>
            </div>
            <div>
                <h2>CFII Time Series</h2>
                <TimeSeriesChart data={timeSeriesData} />
            </div>
            <div>
                <h2>Alerts (CFII > 1)</h2>
                <AlertsTable alerts={alerts} />
            </div>
        </div>
    );
};

export default Dashboard;

