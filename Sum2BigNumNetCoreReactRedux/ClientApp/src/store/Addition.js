const requestType = 'INSERT_CALCULATION';
const receiveType = 'RECEIVE_CALCULATION';

const initialState = { calculation: {}, isLoading: false };


export const actionCreators = {     
    requestCalculationBy: userName => async (dispatch, getState) => {

        const url = `api/Calculation/GetBy?userName=${userName}`;
        const response = await fetch(url);
        console.log(response);
        const calculation = await response.json();

        dispatch({ type: receiveType, calculation });
    },
    insertCalculation: addition => async (dispatch, getState) => {

        const url = 'api/Calculation/Insert';
        const headers = new Headers();
        headers.append('Content-Type', 'application/json');
        const requestOptions = {
            method: 'POST',
            headers,
            body: JSON.stringify(addition)
        };
        const request = new Request(url, requestOptions);
        const calculation = await fetch(request);
        dispatch({ type: requestType, calculation });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === receiveType) {
        return {
            ...state,
            isLoading: false,
            calculation: action.calculation
        };
    }

    if (action.type === requestType) {
        return {
            ...state,
            isLoading: true
        };
    }

    return state;
};