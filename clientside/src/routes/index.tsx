import React from 'react'
import {Route, Routes} from "react-router-dom";
import Home from '../../src/pages/home';
import Details from '../../src/pages/details';


const AppRoutes = () =>
    <Routes>
        <Route  path="/" element={<Home/>}/>
        <Route  path="/details" element={<Details/>}/>
    </Routes>
export default AppRoutes;
