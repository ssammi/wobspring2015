package model.clashgame;
import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Date;

/**
*
* @author Abhijit
*/

public class AttackConfig {
    public int id;
    public List<Integer> speciesIds = Arrays.asList(new Integer[5]);
    public int playerId;
    public Date createdAt;
}