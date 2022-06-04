import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useEffect, useState } from "react";
import { Row, Col, Card, Button, Offcanvas } from "react-bootstrap";
import { Navigate } from "react-router-dom";
import { ListMovie } from "../../app/models/getUserQuery";
import { RequestDeleteMovie } from "../../app/models/userMovieListCommand";

const ListMovieUser = () => {

  const [UserMovieList, setUserMovieList] = useState<ListMovie[]>();
  // const [isDownload, setIsDownload] = useState(false);
  const { user } = useAuth0();
  const url = "https://localhost:7280/api/";
  
  const { getAccessTokenSilently, isAuthenticated } = useAuth0();

  const [token, SetToken] = useState<string>();
  const[result, setResult] = useState(false);

  useEffect(() => {
    const getToken = async () => {
      const domain = "localhost:7280";

      const accessToken = await getAccessTokenSilently({
        audience: `https://${domain}`,
        scope: "read:user",
      });

      SetToken(accessToken);
      
    };

    const authAxios = axios.create({
      baseURL: url,
      headers: {
        Authorization: `Bearer ${token}`,
        Accept: "application/json",
      },
    });

    const fetchUser = async () => {
      try {
        const result = await authAxios.get(`user/getuserlist?email=${user?.email}`);
        setUserMovieList(result.data);
        console.log(result.data);
        // setIsDownload(true);
      } catch (error) {
        console.log(error);
      }
    };

    

    getToken();
    fetchUser();
    
  }, [getAccessTokenSilently, token, user?.email]);

  
  console.log(UserMovieList);

  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const deletAxios = axios.create({
    baseURL: url,
    headers: {
      Authorization: `Bearer ${token}`,
      Accept: "application/json",
    },
  });

  const fetchDeleteMovie = async (movie: number) =>{
    try{
       
      const result = await deletAxios.delete(`user/deletemoviefromlist?movieid=${movie}&emailuser=${user?.email}`);
      console.log(result.status);
      setResult(true);
      setUserMovieList(UserMovieList?.splice(movie))

    }catch(error:any){
      console.log(error)
    }
  };

   const handleClick = (movie:number) => {
    fetchDeleteMovie(movie)
  };
 
  // useEffect(()=>{
    
  //   fetchUser();
  // });

 

  // if(isDownload=== false) return <Spinner animation="grow" style={{position: "absolute", width: 100, top: 50}}/>;
  // if(UserMovieList === null) return (<div>Vous n'avez pas de film dans votre liste...</div>)
 
  return (
    <>
    
    <Row xs={1} md={4} className="g-4" style={{ margin: 30 }}>
      {UserMovieList?.map((movie) => 
        <Col key={movie.id}>
          
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
              variant={!result? "danger": "success"}
              onClick={() => handleClick(movie.id)}
              style={{ marginLeft: 70, marginTop: 10 }}>
                          
              Remove 
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
        </Col>)}
      
    </Row>

    
    
    </>
    
  );
};

export default ListMovieUser;
