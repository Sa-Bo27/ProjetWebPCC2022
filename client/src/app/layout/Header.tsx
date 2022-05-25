import { Navbar, Container, Nav, Button, Modal } from "react-bootstrap";
import LoginButton from "../../features/user/Login";
import LogoutButton from "../../features/user/Logout";
import { useAuth0 } from "@auth0/auth0-react";
import { Avatar } from "@mui/material";
import { useState } from "react";
const Header = () => {
  const { isAuthenticated, user } = useAuth0();

  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  return (
    <>
      <Navbar bg="dark" variant="dark">
        <Container>
          <Navbar.Brand href="/home">ProjetWebPcc</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link href="/home">Home</Nav.Link>
            <Nav.Link href="/catalog">Movie</Nav.Link>
            {isAuthenticated && <Nav.Link href="/mylist">My List</Nav.Link>}
          </Nav>

          <LoginButton />
          <LogoutButton />

          <Button variant="dark" size="sm" onClick={handleShow}>
            <Avatar sx={{ margin: 3 }} src={user?.picture} alt={user?.name} />
          </Button>
        </Container>
      </Navbar>

      {isAuthenticated && (
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title> <Avatar sx={{ margin: 3 }} src={user?.picture} alt={user?.name} /></Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>Name : {user?.name}</p>
          <br></br>
          <p>Email : {user?.email}</p>
          <br></br>
          <p>Birthdate : {user?.birthdate}</p>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
      )}
    </>
  );
};

export default Header;
