
export const Routes = {

    BONDS: 'api/bonds',
    POSITIONS: 'api/positions',
    TRANSACTIONS: 'api/transactions',
    MONEYREPORT: 'api/moneyreport',
    PORTFOLIOREPORT: 'api/portfolioreport',
    FAVOURITEBOND: 'api/FavouriteBond',
    REFRESHDATA: 'api/refresh',
    COUPONEREPORT: 'api/CouponeReport',
    POSITION_FAVORITEREPORT: 'api/Position/favorite',
    POSITION_REPORT: 'api/Position/position',
    POSITION: 'api/Position',
    OBSERVEDPRICES: 'api/ObservedPrices',
    AUTH_LOGIN: 'api/auth/login', 
    AUTH_REGISTER: 'api/auth/register',
    AUTH_REVRESH_TOKEN: 'api/auth/refresh-token',

    getFullUrl: (endpoint: string) => `https://localhost:7071/${endpoint}`,
  };