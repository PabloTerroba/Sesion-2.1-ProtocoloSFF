using System;
using System.Net.Sockets;
using System.Collections.Generic;
using CodecLibrary.Handlers;
using CodecLibrary;
using System.IO;

public abstract class State
{
    protected Dictionary<int, IPacketHandler> _packetHandlerMap;

    public State()
    {
        // Mapear tipos de mensajes a sus manejadores
        _packetHandlerMap = new Dictionary<int, IPacketHandler>();
    }

    public virtual void HandleEvents()
    {
        try
        {
            Packet packet = Receive();
            IPacketHandler handler = _packetHandlerMap[(int)packet.Type];
            handler.Handle(packet);
        }
        catch (KeyNotFoundException e)
        {
            OnUnknownPacket(e);
        }
        catch (SocketException e)
        {
            if (e.SocketErrorCode == SocketError.TimedOut)
                OnTimeout();
            else if (e.SocketErrorCode == SocketError.ConnectionReset)
                OnSocketClosed();
            else
                OnSocketException(e);
        }
        catch (Exception e)
        {
            OnUnknownException(e);
        }
    }

    // Métodos para recibir y manejar eventos
    protected virtual Packet Receive()
    {
        throw new NotImplementedException("Cada estado debe definir su propia lógica de Receive.");
    }
    protected virtual void OnUnknownPacket(Exception e) { /* Manejo de excepciones */ }
    protected virtual void OnTimeout() { /* Manejo de Timeout */ }
    protected virtual void OnSocketClosed() { /* Manejo de cierre de conexión */ }
    protected virtual void OnSocketException(SocketException e) { /* Manejo de excepción de socket */ }
    protected virtual void OnUnknownException(Exception e) { /* Manejo de error desconocido */ }
}
