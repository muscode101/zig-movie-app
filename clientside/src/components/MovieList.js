import React from 'react';
import {useNavigate} from "react-router";

const MovieList = (props) => {
    const navigate = useNavigate();
    return (
        <>
            {props.movies.map((movie, index) => (
                <div className='image-container d-flex justify-content-start m-3'
                     key={index}
                     onClick={() =>{
                         navigate(`/details?id=${movie.id}`);
                     }}>
                    <img src={`https://image.tmdb.org/t/p/w200/${movie.posterPath}`} alt='movie'/>
                </div>
            ))}
        </>
    );
};

export default MovieList;
