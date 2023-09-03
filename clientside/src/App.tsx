import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import AppRoutes from "./routes";
import {BrowserRouter} from "react-router-dom";


const App = () => {
    return (
        <BrowserRouter>
            <div className={'container-fluid movie-app'}>
                <AppRoutes/>
            </div>
        </BrowserRouter>

    );
};

export default App;
