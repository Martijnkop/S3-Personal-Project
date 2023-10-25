import './Nav.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHome, faCog } from '@fortawesome/free-solid-svg-icons';

function Nav() {
    return (
    <div id="sidebar">
        <ul>
        <li>
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