import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

export interface IDetails {
    employeeId: string;
    fullName: number;
    managerId: number;
    dateOfJoining: string;
}

interface IFetchEmployeeDetails {
    details: IDetails[];
    loading: boolean;
}



export class EmployeeDetails extends React.Component<RouteComponentProps<{}>, IFetchEmployeeDetails> {
    constructor() {
        super();
        this.state = { details: [], loading: true };

        fetch('/api/Employee/')
            .then(response => response.json() as Promise<IDetails[]>)
            .then(data => {
                this.setState({ details: data, loading: false });
            });
    }



    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : EmployeeDetails.renderEmployeeDetails(this.state.details);

        return <div>
            <h1>Employee Details</h1>
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

