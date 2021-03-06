import axios from "axios";
import { SetStateAction, useState } from "react";
import { Result } from "../../app/models/movie";
import { InputGroup, Button, FormControl, Stack, Row, Col } from "react-bootstrap";
import MovieCard from "./MovieCard";
import { Typography, Pagination } from "@mui/material";



const Catalog = () => {
  const [movies, setMovies] = useState<Result>();
  const [movieToFind, setMovieTofind] = useState<string>();
  const [page, setPage] = useState<number>();
  const [includeAdulte, setIncludeAdulte] = useState<boolean>();
  const params = new URLSearchParams();
  const nb = 1;
  const baseUrl = "https://localhost:7280/api/";
  
  const [isLoading, setIsLoading] = useState(false);
  

  const movieAxios = axios.create({
    baseURL: baseUrl,
    headers: {
      Accept: "application/json",
    },
  });

  
  //configuration params for request to api

  if (movieToFind) params.append("movieToFind", movieToFind);

  if (page) params.append("page", page.toString());

  if (page === 0) {
    // setPage(1);
    params.append("page", nb.toString());
  }

  if (includeAdulte === null) {
    setIncludeAdulte(true);
    params.append("includeAdulte", includeAdulte);
  }

    //add an hook (useeffect)?

  const fetchMoviesByName = async () => {
    try {
      const result = await movieAxios.get("movie/getmoviesbyname", {
        params: params,
      });

      setMovies(result.data);
    } catch (error) {
      console.log(error);
    }
  };

  const fetchMoviesPopular = async () => {
      try{
            const result = await movieAxios.get("movie/getpopularmovies");
            setMovies(result.data);
      }catch(error){
          console.log(error);
      }
  }

  // search
  const handleSubmit = (e:any) =>{
      e.preventDefault();
      fetchMoviesByName();
  }
 //setpage for pagination 

//  const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
//   setPage(value);
//   fetchMoviesByName();
//  };
  //fetch data for the first time befor searching

  if(!movieToFind && isLoading=== false){ 
      fetchMoviesPopular();
      setIsLoading(true);
}


  return (
    <>
      <Stack gap={2} className="col-md-5 mx-auto" style={{ margin: 20 }}>
        <InputGroup className="mb-3" size="lg" onSubmit={handleSubmit}>
          <FormControl
            placeholder="Movie to find..."
            aria-label="Movie to find..."
            aria-describedby="basic-addon2"
            value={movieToFind}
            onChange={(e) => setMovieTofind(e.target.value)}
          />
          <Button variant="outline-secondary" id="button-addon2" onClick={handleSubmit}>
            Search
          </Button>
        </InputGroup>
      </Stack>
      <Row xs={1} md={4} className="g-4" style={{margin: 30}}>
      {movies?.results.map(movie =>(
          <Col key={movie.id}>
            <MovieCard movie={movie}/>
          </Col>
         ))}
      </Row>
      <br/>
      {/* <Stack  style={{position:'absolute', left:555, }}>
      <Typography>Page: {movies?.page}</Typography>
      <Pagination count={movies?.total_pages} page={page} onChange={handleChange} />
    </Stack> */}
      
    </>
  );
};

export default Catalog;
