import React from 'react';

const AlertsTable = ({ alerts }) => {
    return (
        <table border="1" cellPadding="5" cellSpacing="0">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Region</th>
                    <th>CFII</th>
                </tr>
            </thead>
            <tbody>
                {alerts.length > 0 ? (
                    alerts.map((alert, index) => (
                        <tr key={index}>
                            <td>{alert.date}</td>
                            <td>{alert.regionName}</td>
                            <td>{alert.cfii.toFixed(2)}</td>
                        </tr>
                    ))
                ) : (
                    <tr>
                        <td colSpan="3">No alerts found.</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
};

export default AlertsTable;

