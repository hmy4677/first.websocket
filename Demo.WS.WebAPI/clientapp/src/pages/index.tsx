import { useState, useEffect } from 'react';
import { Button, Input, Space } from 'antd';
import moment from 'moment';

let websocket: WebSocket;
export default function IndexPage() {

  const [wsStatus, setWsStatus] = useState("CLOSED");
  const [userName, setUserName] = useState('');
  const [toUserName, setToUserName] = useState('');
  const [ready, setReady] = useState(false);
  const [text, setText] = useState('');
  const [logs, setLogs] = useState(['']);

  if (websocket) {
    websocket.onopen = e => {
      console.log(e);
    }
    websocket.onclose = e => {
      console.log(e);
    }
    websocket.onmessage = e => {
      setLogs([e.data + '  ' + moment().format('hh:mm:ss'), ...logs]);
    }
  }
  return (
    <div style={{ margin: 20 }}>
      <h1>WebSocket Sample Application</h1>
      <div>
        <p>WebSocked Status:<span style={{ fontWeight: 700 }}>{wsStatus}</span> </p>
        <p>
          <Space>
            <Input placeholder='input your name' style={{ width: 200 }} value={userName} onChange={e => setUserName(e.target.value)} />
            <Input placeholder='input your object name' style={{ width: 200 }} value={toUserName} onChange={e => setToUserName(e.target.value)} />
            <Button onClick={() => {
              setWsStatus('OPENING');
              websocket = new WebSocket(`ws://192.168.1.2:5162/ws?userName=${userName}&toUserName=${toUserName}`);

            }}>Connet
            </Button>
            <Button onClick={() => {
              setWsStatus('CLOSED');
              websocket.close();
            }}
            >Close
            </Button>
          </Space>
        </p>
        <p>
          Message to send:
        </p>
        <p>
          <Space>
            <Input placeholder='input your message'
              style={{ width: 200 }}
              value={text}
              onChange={e => setText(e.target.value)}
            />
            <Button type='primary' onClick={() => {
              websocket.send(text);
              setText('');
            }}>Send
            </Button>
          </Space>

        </p>
      </div>
      <div>
        <h3>Communication Log</h3>
        <div>
          {
            logs.map((item, index) => {
              return (
                <p key={index}>
                  {item}
                </p>
              )
            })
          }
        </div>
      </div>
    </div>
  );
}
