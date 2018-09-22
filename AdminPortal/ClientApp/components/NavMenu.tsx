import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <div className='main-nav'>
            <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={'/'}>Portal</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink to={'/'} exact activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/employeedetails'} activeClassName='active'>
                                <span className='glyphicon glyphicon-user'></span> Employee Details
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/addemployee'} activeClassName='active'>
                                <span className='glyphicon glyphicon glyphicon-plus'></span> Add Employee
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/employeesearch'} activeClassName='active'>
                                <span className='glyphicon glyphicon-search'></span> Search Employee
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}
