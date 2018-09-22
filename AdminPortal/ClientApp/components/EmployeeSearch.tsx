import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { render } from 'react-dom';

import 'isomorphic-fetch';

import { IDetails } from './EmployeeDetails';

export class EmployeeSearch extends React.Component<RouteComponentProps<{}>, any> {
    constructor(props: any) {
        super(props);
        this.state = { details: [], loading: true };
    }

    fetchData = (props: any) => {
        fetch('/api/Employee/Name=' + props.target.value)
            .then(response => response.json() as Promise<IDetails[]>)
            .then(data => {
                if (data === undefined || data.length == 0) {
                    this.setState({ loading: true });
                }
                else {
                    this.setState({ details: data, loading: false });
                }
            });
    }


    public render() {
        let contents = this.state.loading
            ? ''
            : EmployeeSearch.renderEmployeeDetails(this.state.details);

        return <div>
            <h1>Search Employee</h1>
            <div className='input-group'>
                <span className='input-group-addon'><i className='glyphicon glyphicon-user'></i></span>
                <input placeholder='Employee Name' className='form-control' onChange={this.fetchData} type='text' />
            </div>
            {contents}
        </div>;
    }

    private static renderEmployeeDetails(details: IDetails[]) {
        return <div className='table-responsive'><table className='table table-hover table-striped'>
            <thead className='alert alert-info'>
                <tr>
                    <th>Employee Id</th>
                    <th>Full Name</th>
                    <th>Manager Id</th>
                    <th>Date of Joining</th>
                </tr>
            </thead>
            <tbody>
                {details.map(details =>
                    <tr key={details.dateOfJoining}>
                        <td>{details.employeeId}</td>
                        <td>{details.fullName}</td>
                        <td>{details.managerId}</td>
                        <td>{details.dateOfJoining}</td>
                    </tr>
                )}
            </tbody>
        </table></div>;
    }
}

