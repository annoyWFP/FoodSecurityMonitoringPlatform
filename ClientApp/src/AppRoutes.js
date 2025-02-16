import { Dashboard } from "./components/Dashboard";
import { AlertsTable } from "./components/AlertsTable";

const AppRoutes = [
  {
    index: true,
    element: <Dashboard />
  },
  {
    path: '/alertsTable',
    element: <AlertsTable />
  }
];

export default AppRoutes;
