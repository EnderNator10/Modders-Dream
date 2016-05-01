import ('UnityEngine')
import("System")
Debug.Log("[WebTest]Loaded Data From Google.com Data{")

local clock = os.clock
function sleep(n)  -- seconds
  local t0 = clock()
  while clock() - t0 <= n do end
end
url = "http://google.com"
www = WWW(url)
sleep(1)
Debug.Log(www.text)
Debug.Log("}")