import React, { useEffect, useState } from "react";
import { Card, Button, Offcanvas } from "react-bootstrap";
import { Movie } from "../../app/models/movie";
import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { RequestPutMovie } from "../../app/models/userMovieListCommand";
interface Props {
  movie: Movie;
}

const MovieCard = ({ movie }: Props) => {
  const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
  const url = "https://localhost:7280/api/";
  const [token, SetToken] = useState<string>();
  const[send, setSend] = useState(false);
  useEffect(() => {
    const getToken = async () => {
      const domain = "localhost:7280";

      const accessToken = await getAccessTokenSilently({
        audience: `https://${domain}`,
        scope: "write:movie",
      });

      SetToken(accessToken);
    };
    getToken();
  }, [getAccessTokenSilently]);

  const authAxios = axios.create({
    baseURL: url,
    headers: {
      Authorization: `Bearer ${token}`,
      Accept: "application/json",
    },
  });

  const fetchMovieToList = async () => {
    const request: RequestPutMovie = {
      email: user?.email!,
      movieDto: movie,
      id: movie.id,
    };

    try {
      const result = await authAxios.put(
        `user/updateusermovielist/${movie.id}`,
        request
      );
      console.log(result.status);
      setSend(true);
    } catch (error) {
      console.log(error);
    }
    //TODO rajouter le put ver l'api et modifier le end point du controller pour recevoir un objet movie plus email user
  };
  const handleClick = () => {
    fetchMovieToList();
  };

  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  return (
    <>
      <Card style={{ width: "18rem" }}>
        <Card.Img variant="top" src="imgTest.jpg" alt="imgTest.jpg" />
        <Card.Body>
          <Card.Title style={{ fontStyle: "georgia" }}>
            {movie.title}
          </Card.Title>
          <Button
            variant="secondary"
            onClick={handleShow}
            style={{ marginLeft: 60 }}
          >
            Launch resume
          </Button>
          {/* <Card.Text style={{maxHeight: 300}}>{movie.overview}</Card.Text> */}
          {isAuthenticated && (
            <Button
              variant={!send? "secondary": "success"}
              onClick={() => handleClick()}
              style={{ marginLeft: 70, marginTop: 10 }}
            >
              {!send? "Add": "Added"}
              
            </Button>
          )}
        </Card.Body>
      </Card>

      <Offcanvas show={show} onHide={handleClose}>
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>{movie.title}</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <Card.Text style={{ maxHeight: 300 }}>{movie.overview}</Card.Text>
        </Offcanvas.Body>
      </Offcanvas>
    </>
  );
};

export default MovieCard;
