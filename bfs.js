//Perform breadth-first search from initial state, using defined "is_goal_state"
//and "find_successors" functions
//Returns: null if no goal state found
//Returns: object with two members, "actions" and "states", where:
//  actions: Sequence(Array) of action ids required to reach the goal state from the initial state
//  states: Sequence(Array) of states that are moved through, ending with the reached goal state (and EXCLUDING the initial state)
//  The actions and states arrays should both have the same length.
function breadth_first_search(initial_state) {
  let open = []; //See push()/pop() and unshift()/shift() to operate like stack or queue
                 //https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array
  let closed = new Set(); //https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Set

  let actions = [];
  let states = [];
  /*placing the intial state into open set*/
  open.push(initial_state);

 while(open.length >0){
    var state_var = open.shift();
    var aug_state;
    if(state_var==initial_state){
      //defining augmented states
      aug_state ={
        predecessor: null,
        state: state_var,
        action: null
      };
    }else{
      //set it to state_var
      aug_state = state_var;
    }
    if(!closed.has(state_to_uniqueid(aug_state.state))){
        if(!is_goal_state(aug_state.state)){
          let sucs = find_successors(aug_state.state);
          for(let ind=0;ind<sucs.length;ind++){
              let x = Object.assign({},aug_state);
              x.predecessor = aug_state;
              x.state = sucs[ind].resultState;
              x.action = sucs[ind].actionID;
              open.push(x);
          }
          closed.add(state_to_uniqueid(aug_state.state));
        }else{
          break;
        }
    }else{
      continue;
    }
  }
  if(is_goal_state(aug_state.state)){
    helper_eval_state_count--;
    states.unshift(aug_state.state);
    actions.unshift(aug_state.action);
    while(aug_state.predecessor != null){
      aug_state = aug_state.predecessor;
      states.unshift(aug_state.state);
      actions.unshift(aug_state.action);
    }
    states.shift();
    actions.shift();

    return{
      actions: actions,
      states: states
    };
  }else{
    return null;
  }
    /***Your code for breadth-first search here***/

  /*
    Hint: In order to generate the solution path, you will need to augment
      the states to store the predecessor/parent state they were generated from
      and the action that generates the child state from the predecessor state.
      
	  For example, make a wrapper object that stores the state, predecessor and action.
	  Javascript objects are easy to make:
		let object={
			member_name1 : value1,
			member_name2 : value2
		};
      
    Hint: Because of the way Javascript Set objects handle Javascript objects, you
      will need to insert (and check for) a representative value instead of the state
      object itself. The state_to_uniqueid function has been provided to help you with
      this. For example
        let state=...;
        closed.add(state_to_uniqueid(state)); //Add state to closed set
        if(closed.has(state_to_uniqueid(state))) { ... } //Check if state is in closed set
  */
  
  //So you need to start coding here. Don't do it above the hints it will confuse you
  /***Your code to generate solution path here***/

  //define these its used throughout
  
  //OR

  //No solution found
  return null;
}