import { useCallback, useState , useEffect, useRef } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import NavBar from './Components/NavBar'
import LogInMenu from './Components/LogInMenu'
import LogInPopup from './Components/LogInPopup'

function App() {
  const [loginActive , setloginActive] = useState(false)
  const [playerscores , setPlayerScores] = useState({})
  const [isloading , setIsLoading] = useState(true)
  function downloadGame(){
    const handleDownload = async () => {
      try {
        const response = await fetch('https://pbl2-backend-c2aeef5ab7a5.herokuapp.com/download', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/zip',
          },
        });
  
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
  
        const blob = await response.blob();
        const url = window.URL.createObjectURL(blob);
  
        // Create a temporary <a> element to trigger the download
        const link = document.createElement('a');
        link.href = url;
        document.body.appendChild(link);
  
        // Trigger the download
        link.click();
  
        // Clean up the temporary <a> element
        document.body.removeChild(link);
      } catch (error) {
        console.error('Error downloading ZIP file:', error);
      }
  }
  handleDownload()
}
  function loginHandler(){
    setloginActive((prevstate)=> !prevstate)
  }
  function submitLogin(e , username , password){
      e.preventDefault() 
      console.log(username , password)
      // use chorem fetch api to login
        const params = {
          username: username,
          password: password
      };
        fetch("https://pbl2-backend-c2aeef5ab7a5.herokuapp.com/login" , {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(params)
        })
          .then(res => res.json())
          .then(
            (result) => {
              console.log(result)
              if(result.success == true){
                localStorage.setItem("pbl2-cookie" , result.cookie)
              }
            },

            (error) => {
                console.log(error)
            }
          )

  }
  function delteRecord(key){
    // delete record if user is autheticated
    const cookie = localStorage.getItem("pbl2-cookie")
    delete playerscores[key]
    const params = {
      cookie: cookie,
      newrecord: playerscores
  };

  fetch("https://pbl2-backend-c2aeef5ab7a5.herokuapp.com/api/delterecord" , {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(params)
  })
  .then(res => res.json())
  .then(data =>{
    if(data.success == true){
      location.reload()
    }
    else{
      // delte failed
      console.log(data)
      alert(data.message)
    }
  })
  
  }


  useEffect(()=>{
    async function getPlayerScores(){
      var result = await fetch("https://pbl2-backend-c2aeef5ab7a5.herokuapp.com/api/playerscores")
      const jsondata =await result.json()
      setPlayerScores(jsondata.data)
      setIsLoading(false)
    } 
    getPlayerScores()
  },[])

  const table = isloading == false ? 
    (
      <table className="table">
      <thead>
        <tr>
          <th scope="col">index</th>
          <th scope="col">Username</th>
          <th scope="col">score</th>
          <th scope="col">delte record</th>
        </tr>
      </thead>
      <tbody>
        {Object.keys(playerscores).map((key ,i)=>(
              <tr key={playerscores[key]}> 
              <th scope="row">{i+1}</th>
              <td>{key}</td>
              <td>{playerscores[key]}</td>
              <td><button className='btn btn-danger' onClick={()=>delteRecord(key)}>
              <svg xmlns="http://www.w3.org/2000/svg" height="16" width="14" viewBox="0 0 448 512"><path d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z"/>
              </svg>
              </button>
              </td>
            </tr>
        ))}

      </tbody>
    </table>
    )
  :
  <span className="loader"></span>
  return (
    <>
      <NavBar downloadGame={downloadGame} loginHandler={loginHandler}></NavBar>  
      {loginActive && <LogInMenu submitLogin={(e , username , password)=>{submitLogin(e ,username , password)}} loginHandler={loginHandler}></LogInMenu>}   
      <div className='screen'>
        {table}
      </div>
    </>
  )
}

export default App
