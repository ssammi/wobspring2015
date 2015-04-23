/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package net.request.clashgame;

import core.GameServer;
import java.io.DataInputStream;
import java.io.IOException;
import java.util.ArrayList;

import db.clashgame.DefenseConfigDAO;
import net.request.GameRequest;
import net.response.clashgame.ResponseClashPlayerList;
import model.Player;

/**
 *
 * @author lev
 */
public class RequestClashPlayerList extends GameRequest{

    @Override
    public void parse(DataInputStream dataInput) throws IOException {
    }

    @Override
    public void process() throws Exception {
        ResponseClashPlayerList response = new ResponseClashPlayerList();

        ArrayList<Integer> playerIDs = DefenseConfigDAO.allPlayersWithDefenseConfigs();
        for(int pid : playerIDs){
            Player p = GameServer.getInstance().getActivePlayer(pid);
            response.addPlayer(pid, p.getName());
        }

        //need to identify the clash players
        //response.setPlayers(GameServer.getInstance().getActivePlayers());
        
        client.add(response);
    }
    
}
