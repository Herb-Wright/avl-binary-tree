using System;
using System.Collections.Generic;
using System.Text;

namespace avl_binary_tree
{
    class Binary_Tree
    {
        public Node root { get; set; }

        public Binary_Tree()
        {
            root = null;
        }

        public void add(int elem)
        {
            if(root == null)
            {
                root = new Node(elem);
                return;
            }
            root.add(new Node(elem));
            root = root.balance();
        }

        public void delete(int elem)
        {
            if(root == null)
            {
                return;
            }
            if (root.value == elem)
            {
                if (root.right == null)
                {
                    root = root.left;
                    return;
                }
                Node curr = root.right;
                while (curr.left != null)
                {
                    curr = curr.left;
                }
                root.value = curr.value;
                root.right = root.right.delete(curr.value);
            }
            else
            {
                root = root.delete(elem);
            }
            root = root.balance();
        }

        public string to_string()
        {
            if(root == null)
            {
                return "there ain't a tree, bro.";
            }
            return root.to_string(0);
        }
    }

    class Node
    {
        public int value { get; set; }
        public int branch_amount { get; set; }
        public Node left { get; set; }
        public Node right { get; set; }
        
        public Node(int value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
            this.branch_amount = 1;
        }

        public void add(Node to_add)
        {
            if(to_add.value < this.value)
            {
                if(this.left == null)
                {
                    this.left = to_add;
                }
                else
                {
                    this.left.add(to_add);
                }
                this.branch_amount = Math.Max(this.left.branch_amount + 1, this.branch_amount);
                this.left = this.left.balance();
            }
            else
            {
                if (this.right == null)
                {
                    this.right = to_add;
                }
                else
                {
                    this.right.add(to_add);
                }
                this.branch_amount = Math.Max(this.right.branch_amount + 1, this.branch_amount);
                this.right = this.right.balance();
            }
        }

        public int[] get_heights()
        {
            int left_height = 0;
            int right_height = 0;
            if (this.left != null)
            {
                left_height = this.left.branch_amount;
            }
            if (this.right != null)
            {
                right_height = this.right.branch_amount;
            }
            return new int[]{left_height, right_height};
        }

        public Node rotate(bool is_left)
        {
            Node r_val;
            if(is_left)
            {
                Node temp = this.left.right;
                r_val = this.left;
                this.left.right = this;
                this.left = temp;
            }
            else
            {
                Node temp = this.right.left;
                r_val = this.right;
                this.right.left = this;
                this.right = temp;
            }
            int[] heights = this.get_heights();
            this.branch_amount = Math.Max(heights[0], heights[1]) + 1;
            heights = r_val.get_heights();
            r_val.branch_amount = Math.Max(heights[0], heights[1]) + 1;
            return r_val;
        }

        public Node balance()
        {
            Node r_val = this;
            int[] heights = get_heights();
            if (heights[1] > heights[0] + 1)
            {
                int[] right_heights = this.right.get_heights();
                if(right_heights[0] > right_heights[1])
                {
                    this.right = this.right.rotate(true);
                }
                r_val = this.rotate(false);
            }
            else if (heights[0] > heights[1] + 1)
            {
                int[] left_heights = this.left.get_heights();
                if (left_heights[1] > left_heights[0])
                {
                    this.left = this.rotate(false);
                }
                r_val = this.rotate(true);
            }
            return r_val;
        }

        public Node delete(int val)
        {
            if (this.value == val)
            {
                if(this.branch_amount == 1)
                {
                    return null;
                }
                if(this.right == null)
                {
                    return this.left;
                }
                if(this.left == null)
                {
                    return this.right;
                }
                Node curr = this.right;
                while (curr.left != null)
                {
                    curr = curr.left;
                }
                this.value = curr.value;
                this.right = this.right.delete(curr.value);
            }
            else if(this.value > val)
            {
                if(this.left != null)
                {
                    this.left = this.left.delete(val);
                    if(this.left != null)
                    {
                        this.left = this.left.balance();
                    }
                }
            }
            else
            {
                if(this.right != null)
                {
                    this.right = this.right.delete(val);
                    if (this.right != null)
                    {
                        this.right = this.right.balance();
                    }
                }
            }
            int[] heights = this.get_heights();
            this.branch_amount = Math.Max(heights[0], heights[1]) + 1;
            return this;
        }

        public string to_string(int indent)
        {
            string left_string = "";
            string right_string = "";
            char[] tabs = new char[indent];
            for (int i = 0; i < indent; i++)
            {
                tabs[i] = '\t';
            }
            if(left != null)
            {
                left_string = string.Concat("\n", left.to_string(indent + 1));
            }
            if(right != null)
            {
                right_string = string.Concat("\n", right.to_string(indent + 1));
            }
            return string.Format("{0}-{1}{2}{3}", string.Join("",tabs), value, left_string, right_string);
        }
    }
}
