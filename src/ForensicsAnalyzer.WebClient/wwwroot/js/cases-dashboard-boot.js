import {
    initCasesDashboard,
    disposeCasesDashboard,
    setDashboardFocus,
    clearDashboardFocus,
} from "./cases-dashboard.js";

window.ForensicsCasesDashboard = {
    init: initCasesDashboard,
    dispose: disposeCasesDashboard,
    setFocus: setDashboardFocus,
    clearFocus: clearDashboardFocus,
};
