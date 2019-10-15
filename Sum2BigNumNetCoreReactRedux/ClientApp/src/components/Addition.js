import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Growl } from 'primereact/growl';
import { actionCreators } from '../store/Addition';

class Addition extends Component {
    constructor() {
        super();
        this.state = {
            addition: { userName: '', firstNumber: '', secondNumber: '' }
        };
        this.save = this.save.bind(this);
    }

    componentDidUpdate() {
        if (this.props.isLoading) {
            this.props.requestCalculationBy(this.state.addition.userName);
        }
    }

    updateProperty(property, value) {
        console.log('property: ' + property + ', value: ' + value);
        let addition = this.state.addition;
        addition[property] = value;
        this.setState({ addition: addition });
    }

    save() {
        this.props.insertCalculation(this.state.addition);

        this.growl.show({ severity: 'success', detail: "Saved Successfully" });

        //console.log(this.props);
    }

    render() {
        return (
            <React.Fragment>
                <Growl ref={(el) => this.growl = el} />
                <h3>Adding Two Big Numbers</h3>
                <hr />
                <div className="row">
                    <div className="col-md-6">
                        <fieldset>
                            <div className="form-group">
                                <label className="col-md-4 control-label">User Name</label>
                                <div className="col-md-8">
                                    <input type="text" className="form-control" id="userName" name="userName" onChange={(e) => { this.updateProperty('userName', e.target.value) }} />
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <div className="form-group">
                                <label className="col-md-4 control-label">First Number</label>
                                <div className="col-md-8">
                                    <input type="text" className="form-control" id="firstNumber" name="firstNumber" onChange={(e) => { this.updateProperty('firstNumber', e.target.value) }} />
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <div className="form-group">
                                <label className="col-md-4 control-label">Second Number</label>
                                <div className="col-md-8">
                                    <input type="text" className="form-control" id="secondNumber" name="secondNumber" onChange={(e) => { this.updateProperty('secondNumber', e.target.value) }} />
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <div className="col-md-12">
                                <button type="submit" className="btn btn-success text-uppercase" onClick={this.save}>Submit</button>
                            </div>
                        </fieldset>
                    </div>
                    <div className="col-md-6 text-center">
                        <h5 className="text-left">Result :</h5>
                        <table className="table table-bordered">
                            <tbody>
                                <tr>
                                    <td className="width-50 text-left">User Name</td>
                                    <td className="width-50 text-right">{this.state.addition.userName}</td>
                                </tr>
                                <tr>
                                    <td className="text-left">First Number</td>
                                    <td className="text-right">{this.state.addition.firstNumber}</td>
                                </tr>
                                <tr>
                                    <td className="text-left">Second Number</td>
                                    <td className="text-right">{this.state.addition.secondNumber}</td>
                                </tr>
                                <tr>
                                    <td className="text-left text-bold">Summation</td>
                                    <td className="text-right text-bold">{this.props.calculation.Summation}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </React.Fragment>
        );
    }
}

export default connect(
    state => state.addition,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Addition);