import { Home, Settings } from "@mui/icons-material";
import { Divider, IconButton } from "@mui/material";
import { useNavigate } from "react-router-dom";

import './Navbar.jsx.css';

function Navbar() {
    const navigate = useNavigate();

    return (
        <nav className='navbar'>
            <IconButton className="nav-icon" onClick={() => {navigate('/')}}>
                <Home />
            </IconButton>
            <Divider className="nav-divider"/>
            <IconButton className="nav-icon" onClick={() => {navigate('/settings')}}>
            <Settings />
            </IconButton>

        </nav>
    )
}

export default Navbar;