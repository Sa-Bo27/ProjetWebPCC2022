import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useEffect, useState } from "react";
import { Row, Col, Card, Spinner } from "react-bootstrap";
import { UserQuery } from "../../app/models/getUserQuery";

const ListMovieUser = () => {
  const [movieList, setMovieList] = useState<UserQuery>();
  const [isDownload, setIsDownload] = useState(false);
  const { user, getAccessTokenSilently } = useAuth0();
  const url = "https://localhost:7280/api/";
  const AUTH_TOKEN = getAccessTokenSilently();

  const authAxios = axios.create({
    baseURL: url,
    headers: {
      Authorization: `Bearer ${AUTH_TOKEN}`,
      Accept: "application/json",
    },
  });

  const fetchUser = async () => {
    try {
      const result = await authAxios.get(`user/getuser?email=${user?.email}`);
      setMovieList(result.data);
      setIsDownload(true);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(()=>{
    fetchUser();
  });

  if(isDownload=== false) return <Spinner animation="grow" style={{position: "absolute", width: 100, top: 50}}/>;

  return (
    <Row xs={1} md={4} className="g-4" style={{ margin: 30 }}>
      {movieList?.listMovies.map((movie) => (
        <Col key={movie.id}>
          <Card style={{ width: "18rem" }}>
            <Card.Img variant="top" src={movie.poster_path} alt={movie.id.toString()}/>
            <Card.Body>
              <Card.Title>{movie.title}</Card.Title>
              <Card.Text>
                {movie.overview}
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      ))}
    </Row>
  );
};

export default ListMovieUser;
