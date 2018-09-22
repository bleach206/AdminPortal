import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { EmployeeDetails } from './components/EmployeeDetails';
import { AddEmployee } from './components/AddEmployee';
import { EmployeeSearch } from './components/EmployeeSearch';

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/employeedetails' component={EmployeeDetails} />
    <Route path='/addemployee' component={AddEmployee} />
    <Route path='/employeesearch' component={EmployeeSearch} />
</Layout>;
