const requestType = 'REQUEST_CALCULATIONS';
const receiveType = 'RECEIVE_CALCULATIONS';

const initialState = {
    calculations: [],
    isLoading: false
};


export const actionCreators = {
    requestCalculations: () => async (dispatch, getState) => {

        //dispatch({ type: requestType });

        const url = 'api/Calculation/GetAll';
        const response = await fetch(url);
        //console.log(response);
        const calculations = await response.json();

        dispatch({ type: receiveType, calculations });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveType) {
        return {
            ...state,
            calculations: action.calculations,
            isLoading: false
        };
    }

    return state;
};