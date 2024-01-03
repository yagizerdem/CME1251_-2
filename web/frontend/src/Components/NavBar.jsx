

export default function NavBar({downloadGame , loginHandler , deleteDatehandler}){

    return(
        <nav className="top-navbar">
            <ul>
                <li >
                    <button onClick={downloadGame} className="download">
                        Download Game
                    </button>
                </li>
                <li>
                    <button onClick={loginHandler}>LogIn</button>
                </li>
            </ul>
        </nav>
    )
}