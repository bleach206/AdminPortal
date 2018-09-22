import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { render } from 'react-dom';
import { IDetails } from './EmployeeDetails';

class ShowMessage extends React.Component<any, any>{

    constructor(props: any) {
        super(props);
    }

    render() {
        const message = this.props.type === "" ? null : this.props.type === "error" ? this.renderError() : this.renderSuccessful();
        return message;
    }

    renderError() {
        return <div className='alert alert-danger'>
            <strong>Error!</strong> error creating employee record.
                </div>;
    }

    renderSuccessful() {
        return <div className='alert alert-success'>
            <strong>Success!</strong> Employee record created.
              </div>;
    }
}



export class AddEmployee extends React.Component<RouteComponentProps<{}>, any> {
    constructor(props: any) {
        super(props);
        this.state = { managerId: '', fullName: '', showMessage: false, messageType: '' };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event: any) {
        this.setState({ [event.target.name]: event.target.value });
    }

    inputCheck(event: any) {
        let check = this.state.fullName === "" || this.state.managerId === "" ? false : true;
    }

    displayMessage(data: any) {
        this.setState({ showMessage: false, messageType: '' });
        if (data === true) {
            this.setState({ showMessage: true, messageType: 'successful' });
        }
        else {
            this.setState({ showMessage: true, messageType: 'error' });
        }
    }

    handleSubmit(event: any) {

        const errors = AddEmployee.validate(this.state.managerId, this.state.fullName);
        if (!errors.fullName && !errors.managerId) {
            let employee = { FullName: this.state.fullName, ManagerId: this.state.managerId };

            fetch('/api/Employee/', {
                method: 'post',
                body: JSON.stringify(employee),
                headers: {
                    'content-type': 'application/json; charset=utf-8'
                },
            })
                .then(response => response.json())
                .then(data => {
                    this.displayMessage(data);
                });
        }

        event.preventDefault();
    }

    private static validate(managerId: any, fullName: any) {
        return {
            managerId: managerId.length === 0,
            fullName: fullName.length === 0,
        };
    }

    render() {
        const errors = AddEmployee.validate(this.state.managerId, this.state.fullName);

        return (
            <div className='container'>
                <br /><br />
                {this.state.showMessage && <ShowMessage type={this.state.messageType} />}
                <form className='well form-horizontal' onSubmit={this.handleSubmit}>
                    <div className='form-group'>
                        <label className='col-md-4 control-label' >Manager Id</label>
                        <div className='col-md-4 inputGroupContainer'>
                            <div className={errors.managerId ? "input-group has-error" : "input-group"}>
                                <span className='input-group-addon'><i className='glyphicon glyphicon-user'></i></span>
                                <input name='managerId' placeholder='Manager Id' className='form-control' onChange={this.handleChange} type='text' />
                            </div>
                        </div>
                    </div>
                    <div className='form-group'>
                        <label className='col-md-4 control-label'>Full Name</label>
                        <div className='col-md-4 inputGroupContainer'>
                            <div className={errors.fullName ? "input-group has-error" : "input-group"}>
                                <span className='input-group-addon'><i className='glyphicon glyphicon-user'></i></span>
                                <input name='fullName' placeholder='Full Name' className='form-control' type='text' onChange={this.handleChange} />
                            </div>
                        </div>
                    </div>
                    <div className='form-group'>
                        <label className='col-md-4 control-label'></label>
                        <div className='col-md-4'><br />
                            <button type='submit' className='btn btn-primary' >SUBMIT <span className='glyphicon glyphicon-plus'></span></button>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
}