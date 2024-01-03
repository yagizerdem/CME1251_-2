import { useRef, useState } from "react"

export default function LogInMenu({submitLogin , loginHandler}){
    const nameInput = useRef()
    const passwordInput = useRef()
    const [username , setUsername] = useState("")
    const [password , setPassword] = useState("")
    function handleKeyPress(){
        setUsername(nameInput.current.value)
        setPassword(passwordInput.current.value)
    }
    return(
        <div className="card">
            <div className="center">
                <form>
                    <label>enter user name </label>
                    <input type="text" placeholder="username" ref={nameInput} onChange={() =>{handleKeyPress()}}></input>
                    <label>enter password </label>
                    <input type="text" placeholder="password" ref={passwordInput} onChange={() =>{handleKeyPress()}}></input>
                    <button onClick={(e)=>{submitLogin(e , username , password)}}>LogIn</button>
                </form>
                <button onClick={()=>{loginHandler()}} className="closelogin">
                <svg xmlns="http://www.w3.org/2000/svg" height="16" width="12" viewBox="0 0 384 512">
                    <path d="M342.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L192 210.7 86.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L146.7 256 41.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L192 301.3 297.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L237.3 256 342.6 150.6z"/>
                </svg>
                </button> 
            </div>
        </div>
    )
}