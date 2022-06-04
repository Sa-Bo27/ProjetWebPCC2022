import "bootstrap/dist/css/bootstrap.min.css";
import { Routes, Route, BrowserRouter } from "react-router-dom";
import Catalog from "../../features/catalogMovie/Catalog";
import ListMovieUser from "../../features/catalogMovie/ListMovieUser";
import Header from "./Header";
import Home from "./Home";

function App() {
 
  return (
    <>
      <Header />
      <BrowserRouter>
        <Routes>
          <Route path="home" element={<Home />} />
          <Route path="/" element={<Home />} />
          <Route path="catalog" element={<Catalog />} />
          <Route
            path="myList"
            element={<ListMovieUser  />}
          />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
