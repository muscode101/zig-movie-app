import React, {useEffect, useState} from 'react'
import MovieListHeading from "../../components/MovieListHeading";
import SearchBox from "../../components/SearchBox";
import MovieList from "../../components/MovieList";

export default () => {

    const [movies, setMovies] = useState<any[]>([]);
    const [searchValue, setSearchValue] = useState<string>('');

    useEffect(() => {
        getMovieRequest(searchValue);
    }, [searchValue]);

    async function findMovie(searchValue: string) {
        const url = `http://localhost:5266/api/Movie/api/search?query=${searchValue}&include_adult=false&language=en-US&page=1`
        const response = await fetch(url);
        const responseJson = await response.json();
        if (responseJson) {
            setMovies(responseJson);
        }
    }

    const getMovieRequest = async (searchValue: string) => {
        if (searchValue != '') {
            findMovie(searchValue);
        } else {
            getPopularMovies()
        }
    };

    const getPopularMovies = async () => {
        const url = `http://localhost:5266/api/Movie/api/popular`;
        const response = await fetch(url);
        const responseJson = await response.json();
        if (responseJson) {
            setMovies(responseJson);
        }
    };

    return (
            <div className='container-fluid movie-app'>
                <div className='row d-flex align-items-center mt-4 mb-4'>
                    <MovieListHeading heading='Movies'/>
                    <SearchBox searchValue={searchValue} setSearchValue={setSearchValue}/>
                </div>
                <div className='row'>
                    <MovieList
                        movies={movies}
                    />
                </div>
            </div>
    );
}

