import React, {useEffect, useState} from 'react';
import {Circles} from 'react-loader-spinner';

export default () => {
    const queryParameters = new URLSearchParams(window.location.search)
    const id = queryParameters.get("id")
    const [posterPath, setPosterPath] = useState<string>('');
    const [title, setTitle] = useState<string>('');
    const [overview, setOverview] = useState<string>('');
    const [movieUrl, setMovieUrl] = useState<string>('');
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        getMovieById();
    }, []);

    async function getMovieById() {
        const url = `http://localhost:5266/api/Movie/api/movie/${id}`
        const response = await fetch(url);
        const {posterPath, title, overview, homepage} = await response.json();
        console.log('getMovieById', {posterPath, title, overview, homepage})
        if (response) {
            setPosterPath(posterPath);
            setTitle(title);
            setOverview(overview);
            setMovieUrl(homepage);
            setIsLoading(false);
        }
    }


    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <a className="navbar-brand" href="#">Zip Movie App</a>
            </nav>
            {isLoading ?
                <div className={' d-flex justify-content-center align-content-center'}><Circles/></div> : (
                    <div>
                        <div className='d-flex justify-content-start m-3 flex-column'>
                            <img className={'image-container'} src={`https://image.tmdb.org/t/p/w500/${posterPath}`}
                                 alt='movie'/>
                            <div className="media-block">
                                <h3>{title}</h3>
                                <p>
                                    {overview}
                                </p>
                                <a href={movieUrl}>
                                    official site
                                </a>
                            </div>
                        </div>
                    </div>
                )}
        </div>
    );
};


