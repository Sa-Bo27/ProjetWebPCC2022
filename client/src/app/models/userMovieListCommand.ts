
    export interface MovieDto {
        adult: boolean;
        backdrop_path: string;
        id: number;
        original_language: string;
        original_title: string;
        overview: string;
        popularity: number;
        poster_path: string;
        release_date: string;
        title: string;
    }

    export interface RequestPutMovie {
        email: string;
        id: number;
        movieDto: MovieDto;
    }

    export interface RequestDeleteMovie{
        email: string;
        idMovie: number;
    }