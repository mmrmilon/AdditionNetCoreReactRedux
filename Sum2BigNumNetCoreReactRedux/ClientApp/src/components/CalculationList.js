import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Calculation';

class CalculationList extends Component {
    constructor() {
        super();
        
        this.refresh = this.refresh.bind(this);
        this.advanceSearch = this.advanceSearch.bind(this);
    }

    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
        if (this.props.isLoading) {
            this.ensureDataFetched();
        }
    }

    ensureDataFetched() {
        this.props.requestCalculations();
    }

    refresh() {
        this.ensureDataFetched();
    }

    advanceSearch() {
        this.ensureDataFetched();
    }

    render() {

        let userList = this.props.calculations > 0
            && this.props.calculations.map((item, index) => {
                return (
                    <option key={index} value={item.UserId}>{item.UserName}</option>
                )
            }, this);

        //console.log(this.props);
        return (
            <React.Fragment>
                <h3>Calculation Summary</h3>
                <hr />
                <fieldset>
                    <legend style={{ borderBottom: '1px solid #c5c5c5' }}>Advance Filter</legend>
                    <div className="row ">
                        <div className="col-md-4">
                            <fieldset>
                                <div className="row">
                                    <label className="col-md-4 control-label">User Name</label>
                                    <div className="col-md-8">
                                        <select className="form-control">
                                            {userList}
                                        </select>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div className="col-md-5">
                            <fieldset>
                                <div className="row">
                                    <label for="daterange1" className="control-label col-md-5">Calculation Date Range</label>
                                    <div className="col-md-7">
                                        <input id="daterange1" name="daterange1" className="form-control" type="text" />
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div className="col-md-3 text-right">
                            <button type="submit" className="btn btn-success text-uppercase" onClick={this.advanceSearch}>Search</button>&nbsp;
                            <button type="submit" className="btn btn-success text-uppercase" onClick={this.refresh}>Refresh</button>
                        </div>
                    </div>
                </fieldset>
                <hr />
                {renderDataTable(this.props)}
            </React.Fragment>
        );
    }
}

function renderDataTable(props) {
    //console.log(JSON.stringify(props.calculations));
    return (
        <table className='table table-striped table-bordered'>
            <thead>
                <tr>
                    <th className='text-center'>User Name</th>
                    <th className='text-center'>First Number</th>
                    <th className='text-center'>Second Number</th>
                    <th className='text-center'>Summation</th>
                    <th className='text-center'>Calculated On</th>
                </tr>
            </thead>
            <tbody>
                {props.calculations.map(row =>
                    <tr key={row.CalculationId}>
                        <td className='text-center'>{row.UserName}</td>
                        <td className='text-center'>{row.FirstNumber}</td>
                        <td className='text-center'>{row.SecondNumber}</td>
                        <td className='text-center'>{row.Summation}</td>
                        <td className='text-center'>{row.CalculatedOn}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}

export default connect(
    state => state.calculations,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(CalculationList);