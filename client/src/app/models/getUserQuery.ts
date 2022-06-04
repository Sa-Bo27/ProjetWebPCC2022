

export interface ListMovie {
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

export interface UserQuery {
    id: number;
    name: string;
    email: string;
    listMovies: ListMovie[];
}