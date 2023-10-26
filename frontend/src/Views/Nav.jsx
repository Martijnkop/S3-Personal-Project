import './Nav.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHome, faCog } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';

function Nav() {
    const navigate = useNavigate();

    return (
    <div id="sidebar">
        <ul>
        <li onClick={() => {navigate('/')}}>
            <FontAwesomeIcon icon={faHome} className="icon" />
            <h1 href="#">
                Home
            </h1>
        </li>
        <li>
            <FontAwesomeIcon icon={faCog} className="icon" />
            <h1 href="#">
                Settings
            </h1>
        </li>
        </ul>
    </div>)
}

export default Nav;